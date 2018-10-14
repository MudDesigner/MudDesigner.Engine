using MudDesigner.Engine.Components.Actors;
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

        Task Run();
    }
}
