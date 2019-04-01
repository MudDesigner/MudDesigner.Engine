namespace MudDesigner.Engine.Components.Actors
{
    public interface IPlayer : ICharacter
    {
        IActorCommand InitialCommand { get; }

        IServerConnection Connection { get; }
    }
}
