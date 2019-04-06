using MudDesigner.Engine.State.Environment;
using System;

namespace MudDesigner.Engine.Components.Environment
{
    public interface ITimePeriodFactory
    {
        /// <summary>
        /// Looks at a supplied time of day and figures out what <see cref="ITimePeriod"/> needs to be returned that matches the real-world time of day.
        /// </summary>
        /// <param name="currentTime">The current time.</param>
        /// <returns>
        /// Returns an instance of ITimePeriod that represents the current time of day in the game.
        /// </returns>
        ITimePeriod CreateTimePeriodFromDateTime(DateTime currentTime);

        /// <summary>
        /// Evaluates a given world time and returns the time period that the world time is currently in.
        /// </summary>
        /// <param name="currentWorldTime"></param>
        /// <returns></returns>
        ITimePeriod CreateTimePeriodForWorldTime(IWorldTime currentWorldTime);

        ITimePeriod CreateUpcomingTimePeriod(IWorldTime currentWorldTime);

        ITimePeriod CreatePreviousTimePeriod(IWorldTime currentWorldTime);
    }
}
