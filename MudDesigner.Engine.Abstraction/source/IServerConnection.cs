using MudDesigner.Engine.Components.Actors;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    /// <summary>
    /// Represents a clients connection to the server.
    /// The implementation is responsible for parsing the inbound traffic data and publishing it as an instance of <see cref="ICommandInput"/> via eventing.
    /// Adapters elsewhere in the engine can receive it and execute the command
    /// </summary>
    public interface IServerConnection : IInitializable, IDisposable
    {
        IPEndPoint EndPoint { get; }

        Task SendTo(string message);
    }
}
