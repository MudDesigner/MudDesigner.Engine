﻿namespace MudDesigner.Engine.Eventing
{
    /// <summary>
    /// Provides methods for creating a message broker or defining an abstract factory for delegating the creation of a message broker
    /// </summary>
    public interface IEventDispatcherFactory
    {
        IEventDispatcher GetInstance();

        /// <summary>
        /// Creates a new instance of a message broker.
        /// </summary>
        /// <returns>Returns an IMessageBroker implementation</returns>
        IEventDispatcher CreateBroker();
    }
}
