using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MudDesigner.Engine.Components.Actors
{
    public interface IPlayerFactory
    {
        Task<IPlayer> CreatePlayer(IServerConnection connection);
    }
}
