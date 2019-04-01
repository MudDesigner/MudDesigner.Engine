using MudDesigner.Engine.Components.Actors;
using MudDesigner.Engine.Eventing;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public interface IRuntimeHost : IDisposable, IInitializable
    {
        bool IsRunning { get; }

        IGame Game { get; }

        IServer Server { get; }

        string Environment { get; }
    }

    public interface IRuntimeHost<TApp> : IRuntimeHost where TApp : IRuntimeApp
    {
        Task RunAppAsync();

        void HandleEvent<TEvent>() where TEvent : class, IEvent;
    }
}
