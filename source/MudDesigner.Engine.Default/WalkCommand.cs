using MudDesigner.Engine.Components.Actors;
using MudDesigner.Engine.Components.Environment;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    [DisplayName("save")]
    public class SaveCommand : IActorCommand
    {
        public Task<bool> CanProcessCommand(IPlayer source, ICommandInput command, IRuntimeHost runtime)
        {
            if (!runtime.IsRunning)
            {
                return Task.FromResult(false);
            }

            IGame game = runtime.Game;
            TimeTrackingAdapter adapter = game.GetConfiguration()
                .GetAdapters()
                .OfType<TimeTrackingAdapter>()
                .First();

            bool needsSaving = adapter.UpdateFrequency < adapter.UpdateDelta.ElapsedSinceLastUpdate;

            return Task.FromResult(needsSaving);
        }

        public async Task ProcessCommand(IPlayer source, ICommandInput command, IRuntimeHost runtime)
        {
            // Can either send "save" to save yourself, or "save actorName" to save an actor.
            // TODO: Saving anything other than 'self' requires authorization.
            if (command.Arguments.Length > 1)
            {
                // TODO: Figure out how to convey errors back upstream.
                return;
            }

            if (command.Arguments.Length == 0)
            {
                await source.Save();
                return;
            }

            // Search all players logged into the runtime.
            IPlayer[] players = runtime.Game.Players;
            IPlayer playerToSave = players.FirstOrDefault(player => player.Name == command.Arguments[0]);
            if (playerToSave != null)
            {
                await playerToSave.Save();
                return;
            }

            foreach (IRoom room in source.Room.OwningZone.GetRoomsForZone())
            {
                // check each room in the zone for a matching actor to save.
                IActor actor = room.Actors.FirstOrDefault(a => a.Name == command.Arguments[0]);
                if (actor != null)
                {
                    await actor.Save();
                    return;
                }
            }
        }
    }
}
