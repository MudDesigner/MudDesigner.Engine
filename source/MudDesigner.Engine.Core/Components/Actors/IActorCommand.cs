using System.Threading.Tasks;

namespace MudDesigner.Engine.Components.Actors
{
    public interface IActorCommand
    {
        Task<bool> CanProcessCommand(IPlayer source, ICommandInput command, IRuntime runtime);

        Task ProcessCommand(IPlayer source, ICommandInput command, IRuntime runtime);
    }

    public interface ICommandInput
    {
        string Command { get; }

        string[] Arguments { get; }
    }
}
