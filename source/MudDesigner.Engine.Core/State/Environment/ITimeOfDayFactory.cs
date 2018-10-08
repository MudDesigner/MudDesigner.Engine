namespace MudDesigner.Engine.State.Environment
{
    public interface ITimeOfDayFactory
    {
        /// <summary>
        /// Creates a new <see cref="IWorldTime"/> instance.
        /// </summary>
        /// <param name="hour">The hour for the desired time of day.</param>
        /// <param name="minute">The minute for the desired time of day.</param>
        /// <returns>Returns an instance of <see cref="IWorldTime"/></returns>
        IWorldTime Create(double hour, double minute);
    }
}
