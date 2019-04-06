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
        IConfiguration GetConfiguration();
    }
}
