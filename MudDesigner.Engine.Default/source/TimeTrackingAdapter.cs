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

        public bool IsInitialized { get; private set; }

        public bool IsDeleted { get; private set; }

        public Task Delete()
        {
            this.IsDeleted = true;
            throw new NotImplementedException();
        }

        public Task Initialize()
        {
            this.IsInitialized = true;
            return Task.CompletedTask;
        }

        public void SetName(string name)
        {
            throw new NotImplementedException();
        }

        public Task Update(UpdateContext updateContext)
        {
            this.dispatcher.Publish(new TimeUpdatedEvent($"Component is {updateContext.CurrentTime.AliveTime} seconds old."));
            return Task.CompletedTask;
        }
    }
}
