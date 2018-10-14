namespace MudDesigner.Engine.Eventing
{
    /// <summary>
    /// Allows for receiving the content of a message
    /// </summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    public interface IEvent<TContent> : IEvent where TContent : class
    {
        /// <summary>
        /// Gets the content of the message.
        /// </summary>
        TContent Content { get; }
    }
}
