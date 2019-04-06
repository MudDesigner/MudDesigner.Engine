using System;

namespace MudDesigner.Engine
{
    public class AdapterNotFoundException : Exception
    {
        public AdapterNotFoundException(Type adapterType)
        {
            this.ExpectedType = adapterType;
        }

        public Type ExpectedType { get; }
    }
}
