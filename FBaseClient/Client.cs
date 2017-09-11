using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FBaseClient.Model;
using Newtonsoft.Json;

namespace FBaseClient
{
    public class Client
    {
        TcpClient _client = new TcpClient();
        private NetworkStream s;

        public bool Connect(IPAddress adress, int port)
        {
            _client.Connect(adress, port);
            s = _client.GetStream();
            return _client.Connected;
        }

        async public Task<Tuple<string, object>> Query(string cmd)
        {
            var buf = Encoding.UTF8.GetBytes(cmd);
            s.Write(buf,0, buf.Length);
            Tuple<string, object> res =  ProcessAsync().Result;

            return res;
        }

        async Task<byte[]> waitForMessage()
        {
            var buf = new byte[4096];
            await s.ReadAsync(buf, 0, buf.Length);
            return buf;
        }

        public async Task<Tuple<string, object>> ProcessAsync()
        {
            var buf = await waitForMessage();
            var text = Encoding.UTF8.GetString(buf);
            text = text.TrimEnd((char)0x00);

            switch (text.Substring(0,2))
            {
                case "SE":
                    text = text.Remove(0, 2);
                    var entities = JsonConvert.DeserializeObject<List<Entity>>(text);
                    return new Tuple<string, object>("SE", entities);
                    break;
                case "AD":
                    text = text.Remove(0, 2);
                    var id = JsonConvert.DeserializeObject<Guid>(text);
                    return new Tuple<string, object>("AD", id);
                    break;
                case "ER":
                    text = text.Remove(0, 2);
                    return new Tuple<string, object>("ER", text);
                    break;
                case "OK":
                    return new Tuple<string, object>("OK", null);
                    break;
                default:
                    return null;
            }
            
        }
    }
}
