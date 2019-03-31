using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using MudDesigner.Engine;
using MudDesigner.Engine.Eventing;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.ConsoleApp
{
    internal class LogBuilder : ILoggingBuilder
    {
        public IServiceCollection Services => throw new System.NotImplementedException();
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }
        
        private static async Task Run()
        {
            // Setup the event dispatcher
            IEventDispatcherFactory dispatcherFactory = new LocalEventDispatcherFactory();
            IEventDispatcher eventDispatcher = dispatcherFactory.GetInstance();

            // Setup log factory
            ILoggerFactory loggerFactory = Program.CreateLoggerFactory();

            // Setup the component configuration and register it's adapters
            IAdapter timeAdapter = new TimeTrackingAdapter(eventDispatcher);

            var gameConfig = new DefaultGameConfiguration();
            gameConfig.UseAdapter(timeAdapter);
            gameConfig.UseAdapter<WorldAdapter>();

            // Setup the individual engine components
            IGame<DefaultGameConfiguration> game = new DefaultGame();
            game.Configure(gameConfig);

            await game.Initialize();

            // Setup the runtime
            IRuntime runtime = new ConsoleRuntime(eventDispatcher, loggerFactory);
            runtime.HandleEvent<TimeUpdatedEvent>();

            // Start the game loop.
            await runtime.Run(game);
        }

        private static ILoggerFactory CreateLoggerFactory()
        {
            var loggerOptions = new ConsoleLoggerOptions { DisableColors = false, IncludeScopes = true, };

            IConfigureOptions<ConsoleLoggerOptions> configureOptions = new ConfigureOptions<ConsoleLoggerOptions>(options => options.DisableColors = false);
            IPostConfigureOptions<ConsoleLoggerOptions>[] postConfigurationOptions = Array.Empty<IPostConfigureOptions<ConsoleLoggerOptions>>();
            IOptionsFactory<ConsoleLoggerOptions> optionsFactory = new OptionsFactory<ConsoleLoggerOptions>(new[] { configureOptions }, postConfigurationOptions);

            IOptionsMonitorCache<ConsoleLoggerOptions> optionsCache = new OptionsCache<ConsoleLoggerOptions>();
            IOptionsChangeTokenSource<ConsoleLoggerOptions>[] optionsTokenSource = Array.Empty<IOptionsChangeTokenSource<ConsoleLoggerOptions>>();
            IOptionsMonitor<ConsoleLoggerOptions> optionsMonitor = new OptionsMonitor<ConsoleLoggerOptions>(optionsFactory, optionsTokenSource, optionsCache);

            ILoggerProvider consoleProvider = new ConsoleLoggerProvider(options: optionsMonitor);
            ILoggerFactory loggerFactory = new LoggerFactory(new[] { consoleProvider });

            return loggerFactory;
        }
    }
}
