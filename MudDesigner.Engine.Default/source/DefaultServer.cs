using Microsoft.Extensions.Logging;
using MudDesigner.Engine.Components.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public class DefaultServer : IServer
    {
        private readonly List<IPlayer> connectedPlayers = new List<IPlayer>();
        private Socket serverSocket;

        private EngineTimer<IServer> playerTimeoutTimer;
        private CancellationTokenSource listeningTokenSource;
        private Task listeningTask;
        private readonly IPlayerFactory playerFactory;

        private readonly IServerConnectionFactory connectionFactory;

        private readonly ILogger logger;

        public DefaultServer(IPlayerFactory playerFactory, IServerConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
        {
            this.playerFactory = playerFactory;
            this.connectionFactory = connectionFactory;
            this.logger = loggerFactory.CreateLogger<DefaultServer>();
        }

        public int RunningPort { get; private set; } = 5001;

        public IPEndPoint EndPoint { get; private set; }

        public bool IsInitialized { get; private set; }

        public bool IsDeleted { get; private set; }

        public Func<IPlayer, Task> PlayerConnected { get; set; }

        public Func<IPlayer, Task> PlayerDisconnected { get; set; }

        public IActorCommand InitialCommand { get; private set; }

        public async Task Delete()
        {
            this.listeningTokenSource.Cancel();
            foreach (IPlayer player in this.connectedPlayers)
            {
                await player.Connection.Delete();
                await player.Delete();
            }

            this.IsDeleted = true;
        }

        public void Dispose()
        {
            if (!this.listeningTask.IsCompleted)
            {
                this.listeningTokenSource.Cancel();
            }

            this.listeningTask.Dispose();
            this.listeningTokenSource.Dispose();
        }

        public IServerConnection[] GetConnections()
        {
            return this.connectedPlayers.Select(player => player.Connection).ToArray();
        }

        public async Task Initialize()
        {
            this.playerTimeoutTimer = new EngineTimer<IServer>(this);
        }

        public Task RunAsync()
        {
            this.logger.LogInformation("Configuring server for {@listeningInformation}.", new { Host = "localhost", Port = this.RunningPort });
            this.EndPoint = new IPEndPoint(IPAddress.Any, this.RunningPort);

            this.logger.LogInformation("Setting up TCP Connection.");
            this.serverSocket = new Socket(this.EndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.logger.LogInformation("Server bound to host.");
            this.serverSocket.Bind(this.EndPoint);
            this.serverSocket.Listen(50);

            this.logger.LogInformation("Server started.");
            this.ListenForConnection();
            return Task.CompletedTask;
        }

        public void SetInitialCommand(IActorCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), "You can not provide a null initial command.");
            }

            this.InitialCommand = command;
        }

        private void ListenForConnection()
        {
            this.listeningTokenSource = new CancellationTokenSource();
            this.listeningTask = Task.Run(async () =>
            {
                while (!this.IsDeleted)
                {
                    Socket clientSocket = await this.serverSocket.AcceptAsync();
                    await this.CreatePlayerConnection(clientSocket);
                }
            }, this.listeningTokenSource.Token);
        }

        private async Task CreatePlayerConnection(Socket clientConnection)
        {
            IServerConnection playerConnection = await this.connectionFactory.CreateConnection(clientConnection);
            IPlayer player = await this.playerFactory.CreatePlayer(playerConnection);
            if (playerConnection is TcpConnection tcpConnection)
            {
                tcpConnection.SetPlayer(player);
            }

            this.connectedPlayers.Add(player);
            await this.PlayerConnected(player);

            await player.ExecuteCommand(this.InitialCommand);
        }
    }
}
