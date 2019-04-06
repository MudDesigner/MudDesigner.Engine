using System;
using System.Collections.Generic;
using System.Linq;

namespace MudDesigner.Engine
{
    public class DefaultGameConfiguration : IConfiguration
    {
        private readonly List<IAdapter> adapters = new List<IAdapter>();

        public IAdapter GetAdapter<TAdapter>() where TAdapter : IAdapter
        {
            Type requestedAdapterType = typeof(TAdapter);
            for (int index = 0; index < this.adapters.Count; index++)
            {
                IAdapter currentAdapter = this.adapters[index];
                if (currentAdapter.GetType() != requestedAdapterType)
                {
                    continue;
                }

                return (TAdapter)currentAdapter;
            }

            throw new AdapterNotFoundException(requestedAdapterType);
        }

        public IAdapter[] GetAdapters() => this.adapters.ToArray();

        public void UseAdapter<TAdapter>() where TAdapter : class, IAdapter, new()
        {
            this.UseAdapter(new TAdapter());
        }

        public void UseAdapters(IAdapter[] adapters)
        {
            for (int index = 0; index < adapters.ToArray().Length; index++)
            {
                IAdapter adapter = adapters[index];
                this.UseAdapter(adapter);
            }
        }

        public void UseAdapter(IAdapter adapter)
        {
            if (adapter == null)
            {
                throw new ArgumentNullException(nameof(adapter), "Adapter can not be null.");
            }

            Type adapterType = adapter.GetType();
            for (int index = 0; index < this.adapters.ToArray().Length; index++)
            {
                IAdapter currentAdapter = this.adapters[index];
                if (currentAdapter == adapter)
                {
                    return;
                }
                else if (currentAdapter.GetType() == adapterType)
                {
                    // We only allow 1 adapter type at a time to be registered.
                    // We replace the existing one if we're given a new one.
                    this.adapters[index] = adapter;
                    return;
                }
            }

            this.adapters.Add(adapter);
        }
    }
}
