using MudDesigner.Engine;
using MudDesigner.Engine.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Run().GetAwaiter().GetResult();
        }

        static async Task Run()
        {
            var dispatcherFactory = new LocalEventDispatcherFactory();
            var timeAdapter = new TimeTrackingAdapter(dispatcherFactory.GetInstance());

            var gameConfig = new DefaultGameConfiguration();
            gameConfig.UseAdapter(timeAdapter);

            var game = new DefaultGame();
            game.Configure(gameConfig);

            var runtime = new ConsoleRuntime(dispatcherFactory);
            await runtime.RegisterComponent(game);
            
            dispatcherFactory.GetInstance().Subscribe<TimeUpdatedEvent>((@event, sub) =>
            {
                Console.Out.WriteLine(@event.Content);
            });

            await runtime.Run();
        }
    }

    public class ConsoleRuntime : IRuntime
    {
        private readonly IEventDispatcherFactory dispatcherFactory;
        private IGameComponent[] components;

        public ConsoleRuntime(IEventDispatcherFactory dispatcherFactory)
        {
            this.dispatcherFactory = dispatcherFactory;
        }

        public bool IsRunning { get; private set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task RegisterComponent(params IGameComponent[] component)
        {
            this.components = component;
            return Task.CompletedTask;
        }

        public void Pause()
        {

        }

        public void Resume() { }

        public async Task Run()
        {
            var initializingTasks = new List<Task>();
            foreach (IGameComponent component in this.components)
            {
                initializingTasks.Add(component.Initialize());
            }

            await Task.WhenAll(initializingTasks);

            Console.WriteLine("Enter command: ");

            this.IsRunning = true;
            while (this.IsRunning)
            {
                string input = await Console.In.ReadLineAsync();
                Console.Out.WriteLine(input);
            }
        }
    }
}
