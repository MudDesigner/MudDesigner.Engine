using MudDesigner.Engine;
using Serilog;
using Serilog.Events;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.Console
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Starting runtime host");
            var logger = new SerilogLoggerFactory(Log.Logger).CreateLogger(nameof(Program));
            await new RuntimeHost<DefaultApp>(logger).RunAsync();
        }
    }
}
