using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudDesigner.Engine.Components.Actors;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    internal class TcpConnectionFactory : IServerConnectionFactory
    {
        private readonly ILoggerFactory loggerFactory;

        public TcpConnectionFactory(ILoggerFactory loggerFactory) => this.loggerFactory = loggerFactory;

        public async Task<IServerConnection> CreateConnection(Socket socket)
        {
            IServerConnection connection = new TcpConnection(socket, this.loggerFactory);
            await connection.Initialize();
            return connection;
        }
    }

    public class DefaultApp : IRuntimeApp
    {
        public Task AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPlayerFactory, DefaultPlayerFactory>();
            serviceCollection.AddTransient<IServerConnectionFactory, TcpConnectionFactory>();
            serviceCollection.AddSingleton<IGame, DefaultGame>();
            serviceCollection.AddSingleton<IServer, DefaultServer>();
            return Task.CompletedTask;
        }

        public Task Configure(IConfigurationBuilder configurationBuilder)
        {
            return Task.CompletedTask;
        }

        public Task ReceiveRequest(RequestContext requestContext)
        {
            throw new NotImplementedException();
        }
    }
}
