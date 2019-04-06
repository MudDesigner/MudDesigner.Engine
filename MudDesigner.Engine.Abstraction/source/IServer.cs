using MudDesigner.Engine.Components.Actors;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public interface IServer : IDisposable, IInitializable
    {
        Func<IPlayer, Task> PlayerConnected { get; set; }

        Func<IPlayer, Task> PlayerDisconnected { get; set; }

        IPEndPoint EndPoint { get; }

        int RunningPort { get; }

        IActorCommand InitialCommand { get; }

        void SetInitialCommand(IActorCommand command);

        IServerConnection[] GetConnections();

        Task RunAsync();
    }
}
