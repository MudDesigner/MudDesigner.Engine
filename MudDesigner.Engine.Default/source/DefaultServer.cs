using MudDesigner.Engine.Components.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public class DefaultServer : IServer
    {
        private List<IPlayer> connectedPlayers = new List<IPlayer>();
        private Socket serverSocket;

        private EngineTimer<IServer> playerTimeoutTimer;

        private IPlayerFactory playerFactory;

        private IServerConnectionFactory connectionFactory;

        public DefaultServer(IPlayerFactory playerFactory, IServerConnectionFactory connectionFactory)
        {
            this.playerFactory = playerFactory;
            this.connectionFactory = connectionFactory;
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
            foreach(IPlayer player in this.connectedPlayers)
            {
                await player.Delete();
            }

            this.IsDeleted = true;
        }

        public void Dispose()
        {
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
            this.EndPoint = new IPEndPoint(IPAddress.Any, this.RunningPort);

            this.serverSocket = new Socket(this.EndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.serverSocket.Bind(this.EndPoint);
            this.serverSocket.Listen(50);

            return this.ListenForConnection();
        }

        public void SetInitialCommand(IActorCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), "You can not provide a null initial command.");
            }

            this.InitialCommand = command;
        }

        private async Task ListenForConnection()
        {
            Socket clientSocket = await this.serverSocket.AcceptAsync();
            await this.ConnectClient(clientSocket);
        }

        private async Task ConnectClient(Socket clientConnection)
        {
            await this.CreatePlayerConnection(clientConnection);
            await this.ListenForConnection();
        }

        private async Task CreatePlayerConnection(Socket clientConnection)
        {
            IServerConnection playerConnection = await this.connectionFactory.CreateConnection(clientConnection);
            IPlayer player = await this.playerFactory.CreatePlayer(playerConnection);

            this.connectedPlayers.Add(player);
            await this.PlayerConnected(player);

            await player.ExecuteCommand(this.InitialCommand);
        }
    }
}
