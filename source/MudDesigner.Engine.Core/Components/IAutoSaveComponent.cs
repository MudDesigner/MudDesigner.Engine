namespace MudDesigner.Engine.Components
{
    public interface IAutoSaveComponent<TComponentToSave> : IGameComponent where TComponentToSave : class, IComponent
    {
        int AutoSaveFrequency { get; }
    }
}
