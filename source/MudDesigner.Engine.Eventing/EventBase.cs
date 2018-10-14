namespace MudDesigner.Engine.Eventing
{
    /// <summary>
    /// Provides methods for dispatching notifications to subscription handlers
    /// </summary>
    /// <typeparam name="TMessageType">The type of the message type.</typeparam>
    public abstract class EventBase<TContentType> : IEvent<TContentType> where TContentType : class
    {
        /// <summary>
        /// Gets the content of the message.
        /// </summary>
        public TContentType Content { get; protected set; }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <returns>
        /// Returns the content of the message
        /// </returns>
        public object GetContent() => this.Content;
    }
}
