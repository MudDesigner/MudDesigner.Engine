using MudDesigner.Engine.Components.Actors;
using MudDesigner.Engine.Components.Environment;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    internal class DefaultPlayerFactory : IPlayerFactory
    {
        public async Task<IPlayer> CreatePlayer(IServerConnection connection)
        {
            IPlayer player = new DefaultPlayer(connection);
            await player.Initialize();

            return player;
        }
    }

    internal class DefaultPlayer : IPlayer
    {
        public DefaultPlayer(IServerConnection connection) => this.Connection = connection;
        public IServerConnection Connection { get; }

        public IRoom Room => throw new NotImplementedException();

        public IMover Mover => throw new NotImplementedException();

        public IActorCommand LastCommand => throw new NotImplementedException();

        public bool IsStuck => throw new NotImplementedException();

        public bool IsHidden => throw new NotImplementedException();

        public double TimeToLive => throw new NotImplementedException();

        public bool IsImmortal => throw new NotImplementedException();

        public Guid Id => throw new NotImplementedException();

        public bool IsEnabled => throw new NotImplementedException();

        public DateTime CreationDate => throw new NotImplementedException();

        public double TimeAlive => throw new NotImplementedException();

        public bool IsInitialized => throw new NotImplementedException();

        public bool IsDeleted => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Func<IGameComponent, Task> Loading;
        public event EventHandler<EventArgs> Loaded;
        public event Func<IGameComponent, Task> Deleting;
        public event EventHandler<EventArgs> Deleted;

        public void AssignRoom(IRoom newRoom)
        {
            throw new NotImplementedException();
        }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Enable()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IComponent other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IGameComponent other)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteCommand(IActorCommand command)
        {
            return Task.CompletedTask;
        }

        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public void SetConnection(IServerConnection connection)
        {
            throw new NotImplementedException();
        }

        public void SetName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
