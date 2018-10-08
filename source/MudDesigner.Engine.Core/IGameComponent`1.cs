using System;

namespace MudDesigner.Engine
{
    public interface IGameComponent<TConfiguration> : IGameComponent, IEquatable<IGameComponent<TConfiguration>>, IConfigurable<TConfiguration> where TConfiguration : IConfiguration
    {
    }
}
