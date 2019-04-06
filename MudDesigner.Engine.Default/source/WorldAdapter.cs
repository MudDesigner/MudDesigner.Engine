using MudDesigner.Engine.Components.Environment;
using MudDesigner.Engine.Eventing;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public class WorldAdapter : IAdapter
    {
        private readonly IEventDispatcher eventDispatcher;

        public WorldAdapter(/* IWorldStore worldStore ,*/ )
        {
            // TODO: When the data store strategy is determined, provide to the adapter so it can restore the world on initialization.
        }

        public double UpdateFrequency => throw new NotImplementedException();

        public ComponentTime UpdateDelta => throw new NotImplementedException();

        public bool IsInitialized => throw new NotImplementedException();

        public bool IsDeleted => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public Task Initialize()
        {
            throw new NotImplementedException();
        }

        public void SetName(string name)
        {
            throw new NotImplementedException();
        }

        public Task Update(UpdateContext context)
        {
            throw new NotImplementedException();
        }
    }
}
