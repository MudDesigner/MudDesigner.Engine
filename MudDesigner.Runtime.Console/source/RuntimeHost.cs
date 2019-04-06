using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MudDesigner.Engine;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.Console
{
    public class RuntimeHost<TApp> : IRuntimeHost<TApp> where TApp : IRuntimeApp, new()
    {
        private IHost host = null;
        private IRuntimeApp app = null;
        private IServer server = null;

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

            this.server = this.host.Services.GetRequiredService<IServer>();
            await this.server.Initialize();

            this.IsInitialized = true;
        }

        public async Task RunAppAsync()
        {
            if (!this.IsInitialized)
            {
                throw new RuntimeHostNotInitializedException();
            }


            await this.server.RunAsync();
            await this.host.RunAsync();
        }

        void IRuntimeHost<TApp>.HandleEvent<TEvent>()
        {
            throw new NotImplementedException();
        }
    }
}
