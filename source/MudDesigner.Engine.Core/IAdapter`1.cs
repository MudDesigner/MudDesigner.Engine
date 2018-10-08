using System.Collections.Generic;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public interface IAdapter<TConfiguration> : IAdapter, IConfigurable<TConfiguration> where TConfiguration : IConfiguration
    {
    }
}
