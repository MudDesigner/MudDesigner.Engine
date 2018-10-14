using MudDesigner.Engine.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public class DefaultGame : IGameComponent<DefaultGameConfiguration>
    {
        private EngineTimer<IAdapter>[] adapterTimers = Array.Empty<EngineTimer<IAdapter>>();

        public Guid Id { get; } = Guid.NewGuid();

        public bool IsEnabled { get; private set; }

        public DateTime CreationDate { get; } = DateTime.UtcNow;

        public double TimeAlive => DateTime.UtcNow.Subtract(this.CreationDate).TotalSeconds;

        public string Name { get; private set; }

        public string Description { get; set; }

        public DefaultGameConfiguration Configuration { get; private set; }

        public event Func<IGameComponent, Task> Loading;
        public event EventHandler<EventArgs> Loaded;
        public event Func<IGameComponent, Task> Deleting;
        public event EventHandler<EventArgs> Deleted;

        public void Configure(DefaultGameConfiguration configuration)
        {
            this.Configuration = configuration;
            this.adapterTimers = configuration.GetAdapters()
                .Select(adapter => new EngineTimer<IAdapter>(adapter))
                .ToArray();
        }

        public IConfiguration GetConfiguration() => this.Configuration;

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public void Disable()
        {
            this.IsEnabled = false;
            foreach (EngineTimer<IAdapter> adapter in this.adapterTimers)
            {
                adapter.Stop();
            }
        }

        public void Dispose()
        {
            foreach (EngineTimer<IAdapter> adapter in this.adapterTimers)
            {
                adapter.Dispose();
            }
        }

        public void Enable()
        {
            if (this.IsEnabled)
            {
                return;
            }

            foreach (EngineTimer<IAdapter> adapter in this.adapterTimers)
            {
                adapter.Resume();
            }

            this.IsEnabled = true;
        }

        public bool Equals(IComponent other)
        {
            // We don't compare TimeAlive since it is typically a calculated property based off CreationDate.
            return other == this
                || other.CreationDate == this.CreationDate
                && other.Id == this.Id
                && other.IsEnabled == this.IsEnabled;
        }

        public bool Equals(IGameComponent other)
        {
            IComponent otherComponent = other;
            return this.Equals(otherComponent)
                && other.Description == this.Description
                && other.Name == this.Name;
        }

        public async Task Initialize()
        {
            List<Task> initializationTasks = new List<Task>();
            foreach (EngineTimer<IAdapter> adapter in this.adapterTimers)
            {
                initializationTasks.Add(adapter.StateData.Initialize());
            }

            // We need to make sure that the adapters have initialized before we start their timers.
            await Task.WhenAll(initializationTasks);

            foreach (EngineTimer<IAdapter> adapter in this.adapterTimers)
            {
                // We force a minimum update freqency of 1 second.
                double updateFrequency = adapter.StateData.UpdateFrequency > 1000 ? adapter.StateData.UpdateFrequency : 1000;
                adapter.StartAsync(0, updateFrequency, 0, (state, timer) =>
                {
                    double lastUpdateTime = state.UpdateDelta.LastUpdateTime;
                    double updateTime = this.TimeAlive;
                    double timeSinceLastUpdate = updateTime - lastUpdateTime;

                    var componentTime = new ComponentTime(lastUpdateTime, timeSinceLastUpdate, updateTime, this.TimeAlive);
                    return state.Update(componentTime);
                });
            }
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Name is required for this component.");
            }

            this.Name = name;
        }
    }
}
