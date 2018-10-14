namespace MudDesigner.Engine.Eventing
{
    /// <summary>
    /// Allows for receiving the content of a message
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <returns>Returns the content of the message</returns>
        object GetContent();
    }
}
