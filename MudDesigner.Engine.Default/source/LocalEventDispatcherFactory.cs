namespace MudDesigner.Engine.Eventing
{
    public class LocalEventDispatcherFactory : IEventDispatcherFactory
    {
        private readonly IEventDispatcher dispatcher = new LocalEventDispatcher();

        public IEventDispatcher CreateBroker() => new LocalEventDispatcher();

        public IEventDispatcher GetInstance() => this.dispatcher;
    }
}
