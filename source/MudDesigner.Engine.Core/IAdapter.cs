namespace MudDesigner.Engine
{
    /// <summary>
    /// Provides an interface for creating adapters that the game can start and run
    /// </summary>
    public interface IAdapter<TComponent> : IInitializable, IDescriptor, IUpdatable where TComponent : IGameComponent
    {
        /// <summary>
        /// Gets the frequency in seconds that this adapter will get updated.
        /// </summary>
        int UpdateFrequency { get; }
    }
}
