using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MudDesigner.Engine;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.ConsoleApp
{
    public class RuntimeHost<TApp> : IRuntimeHost<TApp> where TApp : IRuntimeApp, new()
    {
        private IHost host = null;

        public bool IsRunning => throw new NotImplementedException();

        public IGame Game => throw new NotImplementedException();

        public IServer Server => throw new NotImplementedException();

        public string Environment => throw new NotImplementedException();

        public bool IsInitialized { get; private set; }

        public bool IsDeleted { get; private set; }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task Initialize()
        {
            IRuntimeApp app = new TApp();

            IHost host = new HostBuilder()
                .ConfigureHostConfiguration(configBuilder =>
                {
                    configBuilder.AddJsonFile("hostsettings.json")
                        .AddEnvironmentVariables(prefix: "mud_");

                    app.Configure(configBuilder);
                })
                .ConfigureServices((hostContext, serviceCollection) =>
                {
                    serviceCollection.AddLogging(logBuilder => logBuilder.AddConsole());
                    serviceCollection.AddSingleton<IGame, DefaultGame>();

                    app.SetHost(this);
                    app.AddServices(serviceCollection);
                })
                .ConfigureLogging(logBuilder => logBuilder.AddConsole())
                .UseConsoleLifetime()
                .Build();

            ILoggerFactory loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();
            app.SetLogFactory(loggerFactory);

            this.host = host;
            this.IsInitialized = true;

            return Task.CompletedTask;
        }

        public async Task RunAppAsync()
        {
            if (!this.IsInitialized)
            {
                throw new RuntimeHostNotInitializedException();
            }
            await this.host.RunAsync();
        }

        void IRuntimeHost<TApp>.HandleEvent<TEvent>()
        {
            throw new NotImplementedException();
        }
    }

    public class RuntimeApp : IRuntimeApp
    {
        public Task AddServices(IServiceCollection serviceCollection)
        {
            throw new NotImplementedException();
        }

        public Task Configure(IConfigurationBuilder configurationBuilder)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveRequest(RequestContext requestContext)
        {
            throw new NotImplementedException();
        }

        public void SetHost(IRuntimeHost host)
        {
            throw new NotImplementedException();
        }

        public void SetLogFactory(ILoggerFactory loggerFactory)
        {
            throw new NotImplementedException();
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            IRuntimeHost<RuntimeApp> runtime = new RuntimeHost<RuntimeApp>();

            await runtime.Initialize();
            await runtime.RunAppAsync();

            //Run().GetAwaiter().GetResult();
        }

        //private static async Task Run()
        //{
        //    // Setup the event dispatcher
        //    IEventDispatcherFactory dispatcherFactory = new LocalEventDispatcherFactory();
        //    IEventDispatcher eventDispatcher = dispatcherFactory.GetInstance();

        //    // Setup log factory
        //    ILoggerFactory loggerFactory = Program.CreateLoggerFactory();

        //    // Setup the component configuration and register it's adapters
        //    IAdapter timeAdapter = new TimeTrackingAdapter(eventDispatcher);

        //    var gameConfig = new DefaultGameConfiguration();
        //    gameConfig.UseAdapter(timeAdapter);
        //    gameConfig.UseAdapter<WorldAdapter>();

        //    // Setup the individual engine components
        //    IGame<DefaultGameConfiguration> game = new DefaultGame();
        //    game.Configure(gameConfig);

        //    await game.Initialize();

        //    // Setup the runtime
        //    IRuntime runtime = new ConsoleRuntime(eventDispatcher, loggerFactory);
        //    runtime.HandleEvent<TimeUpdatedEvent>();

        //    // Start the game loop.
        //    await runtime.Run(game);
        //}

        //private static ILoggerFactory CreateLoggerFactory()
        //{
        //    var loggerOptions = new ConsoleLoggerOptions { DisableColors = false, IncludeScopes = true, };

        //    IConfigureOptions<ConsoleLoggerOptions> configureOptions = new ConfigureOptions<ConsoleLoggerOptions>(options => options.DisableColors = false);
        //    IPostConfigureOptions<ConsoleLoggerOptions>[] postConfigurationOptions = Array.Empty<IPostConfigureOptions<ConsoleLoggerOptions>>();
        //    IOptionsFactory<ConsoleLoggerOptions> optionsFactory = new OptionsFactory<ConsoleLoggerOptions>(new[] { configureOptions }, postConfigurationOptions);

        //    IOptionsMonitorCache<ConsoleLoggerOptions> optionsCache = new OptionsCache<ConsoleLoggerOptions>();
        //    IOptionsChangeTokenSource<ConsoleLoggerOptions>[] optionsTokenSource = Array.Empty<IOptionsChangeTokenSource<ConsoleLoggerOptions>>();
        //    IOptionsMonitor<ConsoleLoggerOptions> optionsMonitor = new OptionsMonitor<ConsoleLoggerOptions>(optionsFactory, optionsTokenSource, optionsCache);

        //    ILoggerProvider consoleProvider = new ConsoleLoggerProvider(options: optionsMonitor);
        //    ILoggerFactory loggerFactory = new LoggerFactory(new[] { consoleProvider });

        //    return loggerFactory;
        //}
    }
}
