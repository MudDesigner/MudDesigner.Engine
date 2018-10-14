using MudDesigner.Engine;
using MudDesigner.Engine.Eventing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.ConsoleApp
{
    public class ConsoleRuntime : IRuntime
    {
        private readonly IEventDispatcherFactory dispatcherFactory;
        private IGameComponent[] components;

        public ConsoleRuntime(IEventDispatcherFactory dispatcherFactory)
        {
            this.dispatcherFactory = dispatcherFactory;
        }

        public bool IsRunning { get; private set; }

        public void Dispose() => this.IsRunning = false;

        public Task RegisterComponent(params IGameComponent[] component)
        {
            this.components = component;
            return Task.CompletedTask;
        }

        public async Task Run()
        {
            var initializingTasks = new List<Task>();
            foreach (IGameComponent component in this.components)
            {
                initializingTasks.Add(component.Initialize());
            }

            await Task.WhenAll(initializingTasks);

            Console.WriteLine("Enter command: ");

            this.IsRunning = true;
            while (this.IsRunning)
            {
                string input = await Console.In.ReadLineAsync();
                Console.Out.WriteLine(input);
            }
        }
    }
}
