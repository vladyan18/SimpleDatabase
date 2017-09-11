using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FBaseClient;
using FBaseClient.Model;
using Newtonsoft.Json;

namespace SimpleClient
{
    class Program
    {
        static FBaseClient.Client _client = new Client();
        static void Main(string[] args)
        {
            System.Console.Out.WriteLine(":=:=:=:=:=:=:=:=:=:PeniSQL:=:=:=:=:=:=:=:=:=:");
            System.Console.Out.WriteLine("Enter IP:");
            var ip = IPAddress.Parse(System.Console.In.ReadLine());
            System.Console.Out.WriteLine("Enter port:");
            var port = int.Parse(System.Console.In.ReadLine());
            System.Console.Out.WriteLine("");
            var c = _client.Connect(ip, port);
            if (c) System.Console.Out.WriteLine("Connected.");
            else System.Console.Out.WriteLine("Connection failed.");

            string cmd = "";
            while (cmd != "exit")
            {
                System.Console.Out.Write(">>");
                cmd = System.Console.In.ReadLine();
                Tuple<string, object> res = _client.Query(cmd).Result;

                switch (res.Item1)
                {
                    case "SE":
                        System.Console.Write(JsonConvert.SerializeObject((List<Entity>)res.Item2, Formatting.Indented));
                        System.Console.Out.WriteLine();
                        break;
                    case "AD":
                        System.Console.Out.WriteLine((Guid)res.Item2);
                        break;
                    case "ER":
                        System.Console.Out.WriteLine("ERROR: " + (string)res.Item2);
                        break;
                }
                System.Console.Out.WriteLine();
            }
        }
    }
}
