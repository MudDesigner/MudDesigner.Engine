using MudDesigner.Engine;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.Console
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IRuntimeHost<RuntimeApp> runtime = new RuntimeHost<RuntimeApp>();

            await runtime.Initialize();
            await runtime.RunAppAsync();
        }
    }
}
