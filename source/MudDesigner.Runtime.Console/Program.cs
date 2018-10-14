using MudDesigner.Engine;
using MudDesigner.Engine.Eventing;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Run().GetAwaiter().GetResult();
        }

        private static async Task Run()
        {
            // Setup the event dispatcher
            IEventDispatcherFactory dispatcherFactory = new LocalEventDispatcherFactory();
            IEventDispatcher eventDispatcher = dispatcherFactory.GetInstance();

            // Setup the component configuration and register it's adapters
            IAdapter timeAdapter = new TimeTrackingAdapter(eventDispatcher);
            var gameConfig = new DefaultGameConfiguration();
            gameConfig.UseAdapter(timeAdapter);

            // Setup the individual engine components
            IGameComponent<DefaultGameConfiguration> game = new DefaultGame();
            game.Configure(gameConfig);

            // Setup the runtime
            IRuntime runtime = new ConsoleRuntime(eventDispatcher);
            await runtime.RegisterComponent(game);

            // Temporary: Register for events from the engine to write to the console.
            eventDispatcher.Subscribe<TimeUpdatedEvent>((@event, sub) =>
            {
                Console.Out.WriteLine(@event.Content);
            });

            // Start the game loop.
            await runtime.Run();
        }
    }
}
