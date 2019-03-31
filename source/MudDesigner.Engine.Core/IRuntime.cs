using MudDesigner.Engine.Components.Actors;
using MudDesigner.Engine.Eventing;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public interface IRuntime : IDisposable
    {
        bool IsRunning { get; }

        IPlayer[] GetPlayers();

        IGameComponent[] GetComponents();

        Task RegisterComponent(params IGameComponent[] component);

        void HandleEvent<TEvent>() where TEvent : class, IEvent;

        Task Run(IGame game);
    }
}
