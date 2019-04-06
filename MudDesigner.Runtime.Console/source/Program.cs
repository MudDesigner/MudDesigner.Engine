using MudDesigner.Engine;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.Console
{
    public class Program
    {
        public static async Task Main(string[] args) => await new RuntimeHost<DefaultApp>().RunAppAsync();
    }
}
