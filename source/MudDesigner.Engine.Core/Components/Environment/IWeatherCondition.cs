namespace MudDesigner.Engine.Components.Environment
{
    /// <summary>
    /// Exposes properties that represent a state that the weather may be in.
    /// </summary>
    public interface IWeatherCondition : IComponent, IDescriptor
    {
        /// <summary>
        /// Gets the occurrence probability of this weather state occurring in an environment.
        /// The higher the probability relative to other weather states, the more likely it is going to occur.
        /// </summary>
        double OccurrenceProbability { get; }
    }
}
