namespace MudDesigner.Engine.Components
{
    public interface IAutoSaveComponent<TConfiguration, TComponentToSave> : IAutoSaveComponent<TComponentToSave>, IGameComponent<TConfiguration> where TConfiguration : IConfiguration where TComponentToSave : class, IComponent
    {

    }
}
