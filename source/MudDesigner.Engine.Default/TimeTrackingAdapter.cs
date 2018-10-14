using MudDesigner.Engine.Eventing;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public class TimeTrackingAdapter : IAdapter
    {
        private readonly IEventDispatcher dispatcher;

        public TimeTrackingAdapter(IEventDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public double UpdateFrequency => 5000;

        public ComponentTime UpdateDelta { get; private set; }

        public string Name => "TIme tracker";

        public string Description { get; set; }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public void SetName(string name)
        {
            throw new NotImplementedException();
        }

        public Task Update(ComponentTime gameTime)
        {
            this.dispatcher.Publish(new TimeUpdatedEvent($"Component is {gameTime.AliveTime} seconds old."));
            return Task.CompletedTask;
        }
    }
}
