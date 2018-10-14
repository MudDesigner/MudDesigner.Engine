namespace MudDesigner.Engine
{
    /// <summary>
    /// Provides an interface for creating adapters that the game can start and run
    /// </summary>
    public interface IAdapter : IInitializable, IDescriptor, IUpdatable
    {
        double UpdateFrequency { get; }

        ComponentTime UpdateDelta { get; }
    }
}
