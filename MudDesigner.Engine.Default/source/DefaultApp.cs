using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudDesigner.Engine.Components.Actors;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    internal class TcpConnectionFactory : IServerConnectionFactory
    {
        public Task<IServerConnection> CreateConnection(Socket socket)
        {
            throw new NotImplementedException();
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
