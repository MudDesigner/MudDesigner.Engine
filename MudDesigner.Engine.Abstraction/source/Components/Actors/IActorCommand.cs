using System.Threading.Tasks;

namespace MudDesigner.Engine.Components.Actors
{
    public interface IActorCommand
    {
        Task<bool> CanProcessCommand(IPlayer source, ICommandInput command, IRuntimeHost runtime);

        Task ProcessCommand(IPlayer source, ICommandInput command, IRuntimeHost runtime);
    }

    public interface ICommandInput
    {
        string Command { get; }

        string[] Arguments { get; }
    }
}
