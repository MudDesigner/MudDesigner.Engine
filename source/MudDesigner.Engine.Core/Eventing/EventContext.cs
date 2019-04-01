using Microsoft.Extensions.Logging;

namespace MudDesigner.Engine.Eventing
{
    public struct EventContext
    {
        public EventContext(IRuntimeHost runtime, IGame game, IEvent triggeredInstance, ISubscription eventSubscription, ILogger logger)
        {
            this.Runtime = runtime;
            this.Game = game;
            this.TriggeredInstance = triggeredInstance;
            this.EventSubscription = eventSubscription;
            this.Logger = logger;
        }

        public IRuntimeHost Runtime { get; }

        public IGame Game { get; }

        public IEvent TriggeredInstance { get; }

        public ISubscription EventSubscription { get; }

        public ILogger Logger { get; }
    }
}
