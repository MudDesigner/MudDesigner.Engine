using Microsoft.Extensions.Logging;
using MudDesigner.Engine;
using MudDesigner.Engine.Components.Actors;
using MudDesigner.Engine.Eventing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MudDesigner.Runtime.ConsoleApp
{
    public class ConsoleRuntime : IRuntime
    {
        private readonly IEventDispatcher eventDispatcher;
        private IGameComponent[] components = Array.Empty<IGameComponent>();

        private ILoggerFactory loggerFactory;
        private ILogger<ConsoleRuntime> logger;

        public ConsoleRuntime(IEventDispatcher eventDispatcher, ILoggerFactory loggerFactory)
        {
            this.eventDispatcher = eventDispatcher;
            this.loggerFactory = loggerFactory;
            this.logger = this.loggerFactory.CreateLogger<ConsoleRuntime>();
        }

        public IGame Game { get; private set; }

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

        public async Task Run(IGame game)
        {
            if (!game.IsInitialized)
            {
                // throw exception
            }
            else if (game.IsDeleted)
            {
                // throw exception
            }

            this.Game = game;

            var initializingTasks = new List<Task>();
            foreach (IGameComponent component in this.components)
            {
                initializingTasks.Add(component.Initialize());
            }

            await Task.WhenAll(initializingTasks);
            this.IsRunning = true;
            while (this.IsRunning)
            {
                // Runtime loop to keep the console running.
            }
        }
        
        public void HandleEvent<TEvent>() where TEvent : class, IEvent
        {
            this.eventDispatcher.Subscribe<TEvent>((@event, subscription) =>
            {
                ILogger eventLogger = this.loggerFactory.CreateLogger(@event.Name);
                var context = new EventContext(this, this.Game, @event, subscription, eventLogger);
                @event.Triggered(context);
            });
        }
    }
}
