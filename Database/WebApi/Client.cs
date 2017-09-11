using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Database.Model;
using Newtonsoft.Json;

namespace Database.WebApi
{
    class Client : IDisposable
    {
        NetworkStream s;
        private string address;
        private readonly Engine.Database _database;

        public Client(TcpClient c, Engine.Database database)
        {
            s = c.GetStream();
            address = Convert.ToString(((System.Net.IPEndPoint)c.Client.RemoteEndPoint).Address);
            _database = database;
        }

        public void Dispose()
        {
            s.Dispose();
        }

        async Task<byte[]> ReadFromStreamAsync(int nbytes)
        {
            var buf = new byte[nbytes];
            var readpos = 0;
            while (readpos < nbytes)
                readpos += await s.ReadAsync(buf, readpos, nbytes - readpos);
            return buf;
        }

        public async Task ProcessAsync()
        {
            var buf = new byte[4086];
            s.Read(buf,0,4086);
            string msg = Encoding.UTF8.GetString(buf);
            msg = msg.TrimEnd((char)0x00);
            
            var commands = msg.Split(',');

            for (int i = 0; i < commands.Length; i++)
            {
                Tuple<string, object> res = _database.DoCommand(commands[i]);

                if (res != null)
                {
                    string jObj = "";
                    switch (res.Item1)
                    {
                        case "ADD":
                            jObj = "AD" + JsonConvert.SerializeObject((Guid)res.Item2);
                            break;

                        case "SELECT":
                            jObj = "SE" + JsonConvert.SerializeObject((List<Entity>) res.Item2);                            
                            break;
                        case "ERROR":
                            jObj = "ER" + (string)res.Item2;
                            break;
                        case "OK":
                            jObj = "OK";
                            break;
                    }

                    var jObjBuf = Encoding.UTF8.GetBytes(jObj);
                    s.Write(jObjBuf, 0, jObjBuf.Length);
                }
            }
        }

 
    }
}
