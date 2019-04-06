using Microsoft.Extensions.Logging;
using MudDesigner.Engine.Eventing;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public class TimeUpdatedEvent : IEvent<string>
    {
        public TimeUpdatedEvent(string content) => this.Content = content;
        public string Content { get; }

        public string Name { get; private set; } = "Engine Time Updated";

        public string Description { get; set; }

        public object GetContent() => this.Content;

        public void SetName(string name) => this.Name = name;

        public Task Triggered(EventContext context)
        {
            context.Logger.LogInformation(Content);
            return Task.CompletedTask;
        }
    }
}
