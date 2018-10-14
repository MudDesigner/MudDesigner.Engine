using MudDesigner.Engine.Eventing;

namespace MudDesigner.Engine
{
    public class TimeUpdatedEvent : IEvent<string>
    {
        public TimeUpdatedEvent(string content) => this.Content = content;
        public string Content { get; }

        public object GetContent() => this.Content;
    }
}
