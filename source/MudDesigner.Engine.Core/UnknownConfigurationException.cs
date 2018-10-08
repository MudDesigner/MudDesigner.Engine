using System;

namespace MudDesigner.Engine
{
    public class UnknownConfigurationException<TExpectedConfiguration> : Exception
    {
        public UnknownConfigurationException(IConfiguration configuration)
        {
            this.GivenConfiguration = configuration;
        }

        /// <summary>
        /// The configuration Type that was expected to of been provided and was not.
        /// </summary>
        public Type ExpectedConfigurationType { get; } = typeof(TExpectedConfiguration);

        /// <summary>
        /// The configuration Type that was provided and not expected.
        /// </summary>
        public IConfiguration GivenConfiguration { get; }
    }
}
