using MudDesigner.Engine;
using MudDesigner.Engine.Eventing;
using System;
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
}
