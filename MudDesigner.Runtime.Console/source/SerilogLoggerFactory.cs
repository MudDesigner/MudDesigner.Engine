using Microsoft.Extensions.Logging;
using Serilog.Debugging;
using Serilog.Extensions.Logging;

namespace MudDesigner.Runtime.Console
{
    internal class SerilogLoggerFactory : ILoggerFactory
    {
        private readonly SerilogLoggerProvider provider;

        internal SerilogLoggerFactory(Serilog.ILogger logger = null, bool dispose = false) => this.provider = new SerilogLoggerProvider(logger, dispose);

        public void AddProvider(ILoggerProvider provider) => SelfLog.WriteLine("Ignoring added logger provider {0}", provider);

        public ILogger CreateLogger(string categoryName) => this.provider.CreateLogger(categoryName);

        public void Dispose() => this.provider.Dispose();
    }
}
