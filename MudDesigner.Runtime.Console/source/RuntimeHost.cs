using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MudDesigner.Engine;
using MudDesigner.Engine.Eventing;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.Console
{
    public class RuntimeHost<TApp> : IRuntimeHost<TApp> where TApp : IRuntimeApp, new()
    {
        private IHost host = null;
        private IRuntimeApp app = null;
        private ILogger logger = null;

        public RuntimeHost(ILogger logger)
        {
            this.logger = logger;
        }

        public bool IsRunning { get; private set; }

        public IGame Game { get; private set; }

        public IServer Server { get; private set; }

        public string Environment { get; private set; }

        public bool IsInitialized { get; private set; }

        public bool IsDeleted { get; private set; }

        public async Task Delete()
        {
            await this.Game.Delete();
            await this.host.StopAsync();

            this.IsRunning = false;
            this.IsInitialized = false;
            this.IsDeleted = true;
        }

        public void Dispose()
        {
            if (!this.IsDeleted)
            {
                throw new DisposingUndeletedHostException("Disposing of a host that has not been deleted is not allowed. Delete the host first.");
            }

            this.host.Dispose();

            this.Game = null;
            this.app = null;
            this.host = null;

        }

        public async Task Initialize()
        {
            this.logger.LogInformation("Initializing Host.");
            this.logger.LogInformation("Using {@AppType} as the hosted application.", new { Name = typeof(TApp).FullName, AppAssembly = typeof(TApp).Assembly.FullName });
            this.app = new TApp();

            IHost host = new HostBuilder()
                .ConfigureHostConfiguration(configBuilder =>
                {
                    var configData = new { HostFile = "hostsettings.json", EnvironmentVariablePrefix = "mud_" };
                    this.logger.LogInformation("Loading {@configData}.", configData);
                    configBuilder.AddJsonFile(configData.HostFile, true)
                        .AddEnvironmentVariables(prefix: configData.EnvironmentVariablePrefix);

                    this.logger.LogInformation("Requesting runtime app to configure itself.");
                    app.Configure(configBuilder);
                })
                .ConfigureLogging(logBuilder =>
                {
                    this.logger.LogInformation("Configuring host logging.");
                    logBuilder.AddConsole();
                })
                .ConfigureServices((hostContext, serviceCollection) =>
                {
                    this.logger.LogInformation("Configuring host services.");
                    serviceCollection.AddLogging(logBuilder => logBuilder.AddConsole());
                    serviceCollection.AddSingleton<ILoggerFactory>(services => new SerilogLoggerFactory());

                    this.logger.LogInformation("Requesting runtime app to configure needed app services.");
                    app.AddServices(serviceCollection);
                })
                .UseConsoleLifetime()
                .Build();

            this.logger.LogInformation("Host initialization completed.");
            // TODO: Need to change this so we pass a MiddlewareCollection so the IRuntimeApp can register instances of IRuntimeMiddleware
            // When the server receives a network request it can pass it into IRuntimeMiddleware
            //app.ReceiveRequest()

            this.host = host;

            this.logger.LogInformation("Setting server up.");
            this.Server = this.host.Services.GetRequiredService<IServer>();
            await this.Server.Initialize();

            this.logger.LogInformation("Setting game up.");
            this.Game = this.host.Services.GetRequiredService<IGame>();
            await this.Game.Initialize();
            this.IsInitialized = true;

            this.logger.LogInformation("Runtime setup completed.");
        }

        public async Task RunAsync()
        {
            if (!this.IsInitialized)
            {
                await this.Initialize();
            }

            this.Server.PlayerConnected = async (newPlayer) =>this.logger.LogInformation("Player connected.");
            this.Server.PlayerDisconnected = async (disconnectedPlayer) => await this.Game.RemovePlayerFromGame(disconnectedPlayer);

            this.IsRunning = true;
            await this.Server.RunAsync();
            await this.host.RunAsync();
        }

        public void HandleEvent<TEvent>() where TEvent : class, IEvent
        {
            throw new NotImplementedException();
        }
    }
}
