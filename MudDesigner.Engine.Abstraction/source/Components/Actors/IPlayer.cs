using System;
using System.Threading.Tasks;
using MudDesigner.Engine.Components.Environment;

namespace MudDesigner.Engine.Components.Actors
{
    public interface IPlayer : ICharacter
    {
        IServerConnection Connection { get; }

        void SetConnection(IServerConnection connection);
    }
}
