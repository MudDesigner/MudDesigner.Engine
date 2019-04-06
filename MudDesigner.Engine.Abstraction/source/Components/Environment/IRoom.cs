using MudDesigner.Engine.Components.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace MudDesigner.Engine.Components.Environment
{
    public interface IRoom
    {
        IZone OwningZone { get; }

        IActor[] Actors { get; }
    }
}
