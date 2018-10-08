namespace MudDesigner.Engine
{
    /// <summary>
    /// Allows a component to become configurable
    /// </summary>
    public interface IConfigurable
    {
        /// <summary>
        /// The current configuration instance.
        /// </summary>
        IConfiguration Configuration { get; }

        /// <summary>
        /// Configures this instance.
        /// </summary>
        /// <param name="configuration">The configuration instance used to apply a configuration with.</param>
        void Configure(IConfiguration configuration);
    }
}
