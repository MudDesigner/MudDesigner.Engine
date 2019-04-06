using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MudDesigner.Engine
{
    public interface IServerConnectionFactory
    {
        Task<IServerConnection> CreateConnection(Socket socket);
    }
}
