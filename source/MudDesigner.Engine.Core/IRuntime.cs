using System;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public interface IRuntime : IDisposable
    {
        bool IsRunning { get; }

        void Pause();
        void Resume();

        Task RegisterComponent(params IGameComponent[] component);

        Task Run();
    }
}
