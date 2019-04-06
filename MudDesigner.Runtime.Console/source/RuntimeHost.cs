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
            this.app = new TApp();

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

                    app.AddServices(serviceCollection);
                })
                .ConfigureLogging(logBuilder => logBuilder.AddConsole())
                .UseConsoleLifetime()
                .Build();

            // TODO: Need to change this so we pass a MiddlewareCollection so the IRuntimeApp can register instances of IRuntimeMiddleware
            // When the server receives a network request it can pass it into IRuntimeMiddleware
            //app.ReceiveRequest()

            this.host = host;

            this.Server = this.host.Services.GetRequiredService<IServer>();
            await this.Server.Initialize();
            this.IsInitialized = true;
        }

        public async Task RunAsync()
        {
            if (!this.IsInitialized)
            {
                await this.Initialize();
            }


            this.Server.PlayerConnected = async (newPlayer) => await this.Game.AddPlayerToGame(newPlayer);
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
