using System;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public interface IRuntime : IDisposable
    {
        bool IsRunning { get; }

        Task RegisterComponent(params IGameComponent[] component);

        Task Run();
    }
}
