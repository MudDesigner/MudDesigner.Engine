using MudDesigner.Engine.Components.Environment;
using System.Threading.Tasks;

namespace MudDesigner.Engine.Components.Actors
{
    public interface IActor : IGameComponent
    {
        IRoom Room { get; }

        IMover Mover { get; }

        /// <summary>
        /// Gets whether or not this actor can travel or not. 
        /// If the current room is null or does not have any doors, then the actor is stuck.
        /// </summary>
        bool IsStuck { get; }

        /// <summary>
        /// Exists and can interact with game objects. 
        /// Game objects can not directly interact with it.
        /// </summary>
        bool IsHidden { get; }

        /// <summary>
        /// If set to 0, then the actor lives until it is destroyed.
        /// Any other value will cause the actor to auto-matically destroy when the time to live has passed.
        /// Value is in seconds.
        /// </summary>
        double TimeToLive { get; }

        /// <summary>
        /// Can never be destroyed.
        /// </summary>
        bool IsImmortal { get; }

        void AssignRoom(IRoom newRoom);

        Task Save();

        Task ExecuteCommand(IActorCommand command);
    }
}
