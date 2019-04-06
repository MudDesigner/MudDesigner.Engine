using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudDesigner.Engine;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.Console
{
    public class RuntimeApp : IRuntimeApp
    {
        public Task AddServices(IServiceCollection serviceCollection)
        {
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
