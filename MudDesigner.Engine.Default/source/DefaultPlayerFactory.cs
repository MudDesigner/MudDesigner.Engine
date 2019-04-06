using MudDesigner.Engine.Components.Actors;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    internal class DefaultPlayerFactory : IPlayerFactory
    {
        public Task<IPlayer> CreatePlayer(IServerConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}
