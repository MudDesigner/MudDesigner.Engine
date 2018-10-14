using MudDesigner.Engine.Components.Environment;
using MudDesigner.Engine.State.Environment;

namespace MudDesigner.Engine.Components.Actors
{
    public interface IMover
    {
        IActor MovableOwner { get; }

        /// <summary>
        /// If this mover is not constrained, than it can move the <see cref="MovableOwner"/> in any direction that the <see cref="MovableOwner"/> has exposed to it within it's current <see cref="IRoom"/>.
        /// </summary>
        /// <param name="allowedDirection"></param>
        void ConstraintMovement(params ITravelDirection[] allowedDirection);

        void Move(ITravelDirection travelDirection);
    }
}
