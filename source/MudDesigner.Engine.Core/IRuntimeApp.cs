using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public interface IRuntimeApp
    {
        Task AddServices(IServiceCollection serviceCollection);

        void SetHost(IRuntimeHost host);

        void SetLogFactory(ILoggerFactory loggerFactory);

        Task Configure(IConfigurationBuilder configurationBuilder);

        Task ReceiveRequest(RequestContext requestContext);
    }
}