namespace MudDesigner.Engine.Eventing
{
    public sealed class LoggingMessage : IEvent<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DebugMessage"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public LoggingMessage(string message)
        {
            this.Content = message;
        }

        public string Content { get; }

        public object GetContent() => this.Content;
    }
}
