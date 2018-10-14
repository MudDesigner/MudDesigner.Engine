using MudDesigner.Engine;
using MudDesigner.Engine.Components.Actors;
using MudDesigner.Engine.Eventing;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.ConsoleApp
{
    public class ConsoleRuntime : IRuntime
    {
        private readonly IEventDispatcher eventDispatcher;
        private IGameComponent[] components;

        public ConsoleRuntime(IEventDispatcher eventDispatcher)
        {
            this.eventDispatcher = eventDispatcher;
        }

        public bool IsRunning { get; private set; }

        public void Dispose() => this.IsRunning = false;

        public IGameComponent[] GetComponents()
        {
            throw new NotImplementedException();
        }

        public IPlayer[] GetPlayers()
        {
            throw new NotImplementedException();
        }

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
