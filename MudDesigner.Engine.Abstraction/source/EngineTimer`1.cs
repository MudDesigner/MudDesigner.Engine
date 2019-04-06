using System;
using System.Threading;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    /// <summary>
    /// <para>
    /// The Engine Timer allows for starting a timer that will execute a callback at a given interval.
    /// </para>
    /// <para>
    /// The timer may fire:
    ///  - infinitely at the given interval
    ///  - fire once
    ///  - fire _n_ number of times.
    /// </para>
    /// <para>
    /// The Engine Timer will stop its self when it is disposed of.
    /// </para>
    /// <para>
    /// The Timer requires you to provide it an instance that will have an operation performed against it.
    /// The callback will be given the generic instance at each interval fired.
    /// </para>
    /// <para>
    /// In the following example, the timer is given an instance of an IPlayer. 
    /// It starts the timer off with a 30 second delay before firing the callback for the first time.
    /// It tells the timer to fire every 60 seconds with 0 as the number of times to fire. When 0 is provided, it will run infinitely.
    /// Lastly, it is given a callback, which will save the player every 60 seconds.
    /// <code>
    /// var timer = new EngineTimer<IPlayer>(new DefaultPlayer());
    /// timer.StartAsync(30000, 6000, 0, (player, timer) => player.Save());
    /// </code>
    /// </para>
    /// </summary>
    /// <typeparam name="TState">The type that will be provided when the timer callback is invoked.</typeparam>
    public sealed class EngineTimer<TState> : CancellationTokenSource, IDisposable where TState : class
    {
        /// <summary>
        /// How many times we have fired the timer thus far.
        /// </summary>
        private long fireCount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="EngineTimer{T}"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="state">The state.</param>
        public EngineTimer(TState state)
        {
            this.StateData = state ?? throw new ArgumentNullException(nameof(state), "EngineTimer constructor requires a non-null argument.");
        }

        /// <summary>
        /// Gets the object that was provided to the timer when it was instanced.
        /// This object will be provided to the callback at each interval when fired.
        /// </summary>
        public TState StateData { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the engine timer is currently running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Sets the timers current state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void SetState(TState state) => this.StateData = state;

        /// <summary>
        /// <para>
        /// Starts the timer, firing a synchronous callback at each interval specified until <code>numberOfFires</code> has been reached.
        /// If <code>numberOfFires</code> is 0, then the callback will be called indefinitely until the timer is manually stopped.
        /// </para>
        /// <para>
        /// The following example shows how to start a timer, providing it a callback.
        /// </para>
        /// <code><![CDATA[
        /// var timer = new EngineTimer<IPlayer>(new DefaultPlayer());
        /// double startDelay = TimeSpan.FromSeconds(30).TotalMilliseconds;
        /// double interval = TimeSpan.FromMinutes(10).TotalMilliseconds;
        /// int numberOfFires = 0;
        /// 
        /// timer.StartAsync(
        ///     startDelay, 
        ///     interval, 
        ///     numberOfFires, 
        ///     async (player, timer) => await player.Save());
        /// ]]></code>
        /// </summary>
        /// <param name="startDelay">
        /// <para>
        /// The <code>startDelay</code> is used to specify how much time must pass before the timer can invoke the callback for the first time.
        /// If 0 is provided, then the callback will be invoked immediately upon starting the timer.
        /// </para>
        /// <para>
        /// The <code>startDelay</code> is measured in milliseconds.
        /// </para>
        /// </param>
        /// <param name="interval">The interval in milliseconds.</param>
        /// <param name="numberOfFires">Specifies the number of times to invoke the timer callback when the interval is reached. Set to 0 for infinite.</param>
        public void StartAsync(double startDelay, double interval, int numberOfFires, Func<TState, EngineTimer<TState>, Task> callback)
        {
            this.StartTimer(
                timerDelegate: (task, state) => this.Run(task, callback, interval, numberOfFires),
                startDelay: startDelay);
        }

        public void Resume() => this.IsRunning = true;

        /// <summary>
        /// Stops the timer for this instance.
        /// Stopping the timer will not dispose of the EngineTimer, allowing you to restart the timer if you need to.
        /// </summary>
        public void Stop() => this.IsRunning = false;

        /// <summary>
        /// Stops the timer and releases the unmanaged resources used by the <see cref="T:System.Threading.CancellationTokenSource" /> class and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Stop();
            }
            
            if (!base.IsCancellationRequested)
            {
                base.Cancel();
            }

            base.Dispose(disposing);
        }

        private void StartTimer(Func<Task, TState, Task> timerDelegate, double startDelay)
        {
            this.IsRunning = true;

            Task callback(Task task, object state) => timerDelegate(task, (TState)state);

            Task.Delay(TimeSpan.FromMilliseconds(startDelay), this.Token)
                .ContinueWith(
                    callback,
                    this.StateData,
                    this.Token,
                    TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.Default);
        }

        private async Task Run(Task task, Func<TState, EngineTimer<TState>, Task> callback, double interval, int numberOfFires)
        {
            while (!this.IsCancellationRequested)
            {
                // Only increment if we are supposed to.
                if (numberOfFires > 0)
                {
                    this.fireCount++;
                }

                if (this.IsRunning)
                {
                    await callback(this.StateData, this);
                }

                PerformTimerCancellationCheck(interval, numberOfFires);
                await Task.Delay(TimeSpan.FromMilliseconds(interval), this.Token).ConfigureAwait(false);
            }
        }

        private void PerformTimerCancellationCheck(double interval, int numberOfFires)
        {
            // If we have reached our fire count, stop. If set to 0 then we fire until manually stopped.
            if (numberOfFires > 0 && this.fireCount >= numberOfFires)
            {
                this.Stop();
            }
        }
    }
}
