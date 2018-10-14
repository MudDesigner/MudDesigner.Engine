using System;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public interface IGameComponent : IComponent, IInitializable, IDescriptor, IEquatable<IGameComponent>
    {
        /// <summary>
        /// The Loading event is fired during initialization of the component prior to being loaded.
        /// </summary>
        event Func<IGameComponent, Task> Loading;

        /// <summary>
        /// The Loaded event is fired upon completion of the components initialization and loading.
        /// </summary>
        event EventHandler<EventArgs> Loaded;

        /// <summary>
        /// The Deleting event is fired immediately upon a delete request.
        /// </summary>
        event Func<IGameComponent, Task> Deleting;

        /// <summary>
        /// The Deleted event is fired once the object has finished processing it's unloading and clean up.
        /// </summary>
        event EventHandler<EventArgs> Deleted;
    }

    /// <summary>
    /// Provides methods, events and properties for interacting with a component during it's life-cycle.
    /// </summary>
    public interface IGameComponent<TConfiguration> : IGameComponent, IConfigurable<TConfiguration> where TConfiguration : IConfiguration
    {
    }
}
