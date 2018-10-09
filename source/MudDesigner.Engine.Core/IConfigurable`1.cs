namespace MudDesigner.Engine
{
    /// <summary>
    /// Allows a component to become configurable using a strongly typed configuration class
    /// </summary>
    /// <typeparam name="TConfiguration">The type of the configuration.</typeparam>
    public interface IConfigurable<TComponent, TConfiguration> : IConfigurable<TComponent> where TConfiguration : IConfiguration<TComponent> where TComponent : IComponent
    {
        /// <summary>
        /// Configures the component using the given configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        void Configure(TConfiguration configuration);

        /// <summary>
        /// Attempts to return the current configuration as the strongly typed instance of <see cref="TConfiguration"/>
        /// </summary>
        /// <exception cref="UnknownConfigurationException{TExpectedConfiguration}">Thrown when the configuration given to this instance does not match the Type for the generic parameter.</exception>
        /// <returns>Returns the current configuration if it is of Type <see cref="TConfiguration"/>, otherwise throws <see cref="UnknownConfigurationException{TExpectedConfiguration}"/></returns>
        TConfiguration GetConfiguration();
    }
}
