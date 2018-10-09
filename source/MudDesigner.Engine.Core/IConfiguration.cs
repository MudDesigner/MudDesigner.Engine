namespace MudDesigner.Engine
{
    /// <summary>
    /// Provides methods for components to use when they want to be used to configure another object.
    /// </summary>
    public interface IConfiguration<TComponent> where TComponent : IComponent
    {
        TComponent Component { get; }

        /// <summary>
        /// Gets the game adapter components that have been registered.
        /// </summary>
        /// <returns>Returns an array of adapter components</returns>
        IAdapter<TComponent>[] GetAdapters();

        IAdapter<TComponent> GetAdapter<TAdapter>() where TAdapter : IAdapter<TComponent>;

        /// <summary>
        /// Tells the game configuration that a specific adapter component must be used by the game.
        /// A new instance of TConfigComponent will be created when the game starts.
        /// The adapter must have a parameterless constructor, otherwise instantiation will throw an exception.
        /// </summary>
        /// <typeparam name="TAdapter">The type of the adapter component to use.</typeparam>
        void UseAdapter<TAdapter>() where TAdapter : class, IAdapter<TComponent>, new();

        /// <summary>
        /// Tells the game configuration that a specific adapter component must be used by the game.
        /// </summary>
        /// <typeparam name="TAdapter">The type of the adapter component.</typeparam>
        /// <param name="component">The component instance you want to use.</param>
        void UseAdapter<TAdapter>(TAdapter component) where TAdapter : class, IAdapter<TComponent>;

        /// <summary>
        /// Tells the game configuration that specific adapter components must be used by the game.
        /// </summary>
        /// <param name="adapters">The adapters.</param>
        void UseAdapters(IAdapter<TComponent>[] adapters);
    }
}
