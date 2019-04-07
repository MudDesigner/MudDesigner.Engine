using Microsoft.Extensions.Logging;
using MudDesigner.Engine.Components.Actors;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public static class SocketExtensions
    {
        public static Task<SocketAsyncEventArgs> PerformAsyncOperation(this Socket socket, Action<SocketAsyncEventArgs> operation, byte[] buffer)
        {
            // Configure the async callback event.
            var completionSource = new TaskCompletionSource<SocketAsyncEventArgs>();
            var asyncArgs = new SocketAsyncEventArgs();
            asyncArgs.Completed += (sender, args) => completionSource.SetResult(args);
            asyncArgs.SetBuffer(buffer, 0, buffer.Length);
            // Invoke what ever socket operation is being used with the args.
            operation(asyncArgs);

            return completionSource.Task;
        }
    }

    internal class TcpConnection : IServerConnection
    {
        private readonly Socket socket;
        private readonly ILogger<TcpConnection> logger;
        private byte[] buffer;
        private CancellationTokenSource receivingDataCancellationToken;
        private Task receivingDataTask;

        public TcpConnection(Socket connection, ILoggerFactory loggerFactory)
        {
            this.socket = connection;
            this.logger = loggerFactory.CreateLogger<TcpConnection>();
        }

        public IPEndPoint EndPoint => throw new NotImplementedException();

        public bool IsInitialized => throw new NotImplementedException();

        public bool IsDeleted => throw new NotImplementedException();

        public IPlayer Owner { get; private set; }
        public int BufferSize { get; private set; }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task Initialize()
        {
            this.logger.LogInformation("TCP client connection initializing");
            this.buffer = new byte[this.BufferSize];

            this.receivingDataCancellationToken = new CancellationTokenSource();
            this.receivingDataTask = Task.Run(async () =>
            {
                while (this.IsInitialized)
                {
                    var asyncArg = await this.socket.PerformAsyncOperation(arg => this.socket.ReceiveAsync(arg), this.buffer);
                    int bytesRead = asyncArg.BytesTransferred;
                    string message = System.Text.Encoding.UTF8.GetString(asyncArg.Buffer);
                    this.logger.LogInformation("{@data} received from client.", new { Message = message, Bytes = bytesRead, BufferSize = asyncArg.Buffer.Length, ClientIP = asyncArg.RemoteEndPoint.ToString() });
                }
            }, this.receivingDataCancellationToken.Token);

            return Task.CompletedTask;
        }

        public Task SendTo(string message)
        {
            throw new NotImplementedException();
        }

        internal void SetPlayer(IPlayer player)
        {
            this.Owner = player;
        }
    }
}
