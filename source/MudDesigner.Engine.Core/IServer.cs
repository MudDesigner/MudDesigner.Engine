﻿using MudDesigner.Engine.Components.Actors;
using System;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public interface IServer : IDisposable, IInitializable, IConfigurable
    {
        IServerConnection[] GetConnections();

        IServerConnection GetConnectionForPlayer(IPlayer player);

        Task RunAsync();
    }
}
