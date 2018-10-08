namespace MudDesigner.Engine.Components.Environment
{
    public interface IWorld<TConfiguration> : IWorld, IGameComponent<TConfiguration> where TConfiguration : IConfiguration
    {
    }
}
