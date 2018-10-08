using MudDesigner.Engine.State.Environment;
using System;

namespace MudDesigner.Engine.Components.Environment
{
    public interface ITimePeriod : IComponent, IDescriptor, IInitializable
    {
        event EventHandler<IWorldTime> TimeUpdated;

        IWorldTime CurrentTime { get; }

        IWorldTime StartTime { get; }
    }

    public interface ITimePeriod<TConfiguration> : ITimePeriod, IConfigurable<TConfiguration> where TConfiguration : IConfiguration
    {
    }
}
