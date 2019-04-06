using MudDesigner.Engine.Components.Actors;
using MudDesigner.Engine.Components.Environment;
using MudDesigner.Engine.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public class DefaultGame : IGame<DefaultGameConfiguration>
    {
        private EngineTimer<IAdapter>[] adapterTimers = Array.Empty<EngineTimer<IAdapter>>();
        private List<IPlayer> players = new List<IPlayer>();

        public event Func<IGameComponent, Task> Loading;
        public event EventHandler<EventArgs> Loaded;
        public event Func<IGameComponent, Task> Deleting;
        public event EventHandler<EventArgs> Deleted;

        public Guid Id { get; } = Guid.NewGuid();

        public IPlayer[] Players => this.players.ToArray();

        public bool IsEnabled { get; private set; }

        public DateTime CreationDate { get; } = DateTime.UtcNow;

        public double TimeAlive => DateTime.UtcNow.Subtract(this.CreationDate).TotalSeconds;

        public string Name { get; private set; }

        public string Description { get; set; }

        public DefaultGameConfiguration Configuration { get; private set; }

        public bool IsInitialized { get; private set; }

        public bool IsDeleted { get; private set; }

        public IConfiguration GetConfiguration() => this.Configuration;

        public void Configure(DefaultGameConfiguration configuration)
        {
            this.Configuration = configuration;
            this.adapterTimers = configuration.GetAdapters()
                .Select(adapter => new EngineTimer<IAdapter>(adapter))
                .ToArray();
        }

        public Task Delete()
        {
            this.IsDeleted = true;
            throw new NotImplementedException();
        }

        public void Disable()
        {
            this.IsEnabled = false;
            foreach (EngineTimer<IAdapter> adapter in this.adapterTimers)
            {
                adapter.Stop();
            }
        }

        public void Dispose()
        {
            foreach (EngineTimer<IAdapter> adapter in this.adapterTimers)
            {
                adapter.Dispose();
            }
        }

        public void Enable()
        {
            if (this.IsEnabled)
            {
                return;
            }

            foreach (EngineTimer<IAdapter> adapter in this.adapterTimers)
            {
                adapter.Resume();
            }

            this.IsEnabled = true;
        }

        public bool Equals(IComponent other)
        {
            // We don't compare TimeAlive since it is typically a calculated property based off CreationDate.
            return other == this
                || other.CreationDate == this.CreationDate
                && other.Id == this.Id
                && other.IsEnabled == this.IsEnabled;
        }

        public bool Equals(IGameComponent other)
        {
            IComponent otherComponent = other;
            return this.Equals(otherComponent)
                && other.Description == this.Description
                && other.Name == this.Name;
        }

        public async Task Initialize()
        {
            List<Task> initializationTasks = new List<Task>();
            foreach (EngineTimer<IAdapter> adapter in this.adapterTimers)
            {
                initializationTasks.Add(adapter.StateData.Initialize());
            }

            // We need to make sure that the adapters have initialized before we start their timers.
            await Task.WhenAll(initializationTasks);

            foreach (EngineTimer<IAdapter> adapter in this.adapterTimers)
            {
                // We force a minimum update freqency of 1 second.
                double updateFrequency = adapter.StateData.UpdateFrequency > 1000 ? adapter.StateData.UpdateFrequency : 1000;
                adapter.StartAsync(0, updateFrequency, 0, (state, timer) =>
                {
                    double lastUpdateTime = state.UpdateDelta.LastUpdateTime;
                    double updateTime = this.TimeAlive;
                    double timeSinceLastUpdate = updateTime - lastUpdateTime;

                    var componentTime = new ComponentTime(lastUpdateTime, timeSinceLastUpdate, updateTime, this.TimeAlive);
                    var updateContext = new UpdateContext(this, componentTime);
                    return state.Update(updateContext);
                });
            }

            this.IsInitialized = true;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Name is required for this component.");
            }

            this.Name = name;
        }

        public IWorld[] GetWorldsInGame()
        {
            throw new NotImplementedException();
        }

        public Task<IWorld> CreateWorld(string name)
        {
            throw new NotImplementedException();
        }

        public Task AddWorldsToGame(IWorld[] worlds)
        {
            throw new NotImplementedException();
        }

        public Task AddWorldToGame(IWorld world)
        {
            throw new NotImplementedException();
        }

        public Task RemoveWorldFromGame(IWorld world)
        {
            throw new NotImplementedException();
        }

        public Task RemoveWorldsFromGame(IWorld[] worlds)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IGame other)
        {
            throw new NotImplementedException();
        }

        public Task AddPlayerToGame(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public Task RemovePlayerFromGame(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
