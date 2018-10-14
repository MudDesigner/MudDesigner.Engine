namespace MudDesigner.Engine
{
    /// <summary>
    /// Provides methods for components to use when they want to be used to configure another object.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the game adapter components that have been registered.
        /// </summary>
        /// <returns>Returns an array of adapter components</returns>
        IAdapter[] GetAdapters();

        IAdapter GetAdapter<TAdapter>() where TAdapter : IAdapter;

        void UseAdapter(IAdapter adapter);

        /// <summary>
        /// Tells the game configuration that a specific adapter component must be used by the game.
        /// A new instance of TConfigComponent will be created when the game starts.
        /// The adapter must have a parameterless constructor, otherwise instantiation will throw an exception.
        /// </summary>
        /// <typeparam name="TAdapter">The type of the adapter component to use.</typeparam>
        void UseAdapter<TAdapter>() where TAdapter : class, IAdapter, new();

        /// <summary>
        /// Tells the game configuration that specific adapter components must be used by the game.
        /// </summary>
        /// <param name="adapters">The adapters.</param>
        void UseAdapters(IAdapter[] adapters);
    }
}
