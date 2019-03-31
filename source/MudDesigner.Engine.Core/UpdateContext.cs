namespace MudDesigner.Engine
{
    public struct UpdateContext
    {
        public UpdateContext(IGame game, ComponentTime currentTime)
        {
            this.Game = game;
            this.CurrentTime = currentTime;
        }

        public IGame Game { get; }

        public ComponentTime CurrentTime { get; }
    }
}
