using System;

namespace MudDesigner.Engine.Fixtures
{
    internal class ComponentFixture : IComponent
    {
        public Guid Id => throw new NotImplementedException();

        public bool IsEnabled => throw new NotImplementedException();

        public DateTime CreationDate => throw new NotImplementedException();

        public double TimeAlive => throw new NotImplementedException();

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Enable()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IComponent other)
        {
            throw new NotImplementedException();
        }
    }
}
