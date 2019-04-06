using MudDesigner.Engine.Components.Actors;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public class DefaultServer : IServer
    {
        public bool IsInitialized { get; private set; }

        public bool IsDeleted { get; private set; }

        public Task Delete()
        {
            this.IsDeleted = true;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IServerConnection GetConnectionForPlayer(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public IServerConnection[] GetConnections()
        {
            throw new NotImplementedException();
        }

        public async Task Initialize()
        {
            throw new NotImplementedException();
        }

        public Task RunAsync()
        {
            throw new NotImplementedException();
        }
    }

}
