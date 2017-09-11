using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Database.WebApi
{
    public class Server
    {
        private readonly Engine.Database _database;

        public Server(Engine.Database database)
        {
            _database = database;
        }

        public async void Run()
        {
            var tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7777);
            tcpListener.Start();
            while (true)
            {
                var tcpClient = await tcpListener.AcceptTcpClientAsync();
                _processClientTearOff(tcpClient);
            }

        }

        private async Task _processClientTearOff(TcpClient c)
        {
            using (var client = new Client(c, _database))
            {   
                while (c.Connected)
                await client.ProcessAsync();
            }
        }
    }
}
