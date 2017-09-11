using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Database
{
    class Program
    {
        static Engine.Database _db = new Engine.Database();

        static void Main(string[] args)
        {
            mainMenu();
        }

        private static void mainMenu()
        {
            System.Console.Clear();

            System.Console.Out.WriteLine(":=:=:=:=:=:=:=:=:=:SuperSQL:=:=:=:=:=:=:=:=:=:");
            System.Console.Out.WriteLine("1. Create new database.");
            System.Console.Out.WriteLine("2. Open existing database.");
            System.Console.Out.WriteLine("3. Exit.");

            try
            {

                int c = int.Parse(System.Console.In.ReadLine());


                string name;
                switch (c)
                {
                    case 1:
                        System.Console.Out.WriteLine("Enter the database name:");
                        name = System.Console.In.ReadLine();

                        _db.Create(name);
                        _db.Run();
                        break;
                    case 2:
                        System.Console.Out.WriteLine("Enter the database name:");
                        name = System.Console.In.ReadLine();

                        _db.Open(name);
                        _db.Run();
                        break;
                    case 3:
                        return;
                        break;
                    default:
                        throw new ArgumentException("Invalid command");
                        break;
                }

                System.Console.Out.WriteLine("");
                System.Console.Out.WriteLine("Commands:");

                processCommands();
            } catch (Exception e) { mainMenu();}
        }

        private static void processCommands()
        {
            string cmd = "";
            while (!cmd.Equals("exit"))
            {
                System.Console.Out.Write(">>");
                cmd = System.Console.In.ReadLine();
                if (cmd.Equals("exit")) break;
                else
                {
                    var res = _db.DoCommand(cmd);
                    if (res != null)
                    {
                        if (res.Item1.Equals("ERROR"))
                            System.Console.Out.WriteLine("ERROR: " + (string)res.Item2);
                        else if (!res.Item1.Equals("OK"))
                        {
                            System.Console.Write(JsonConvert.SerializeObject(res.Item2, Formatting.Indented));
                        }
                        System.Console.Out.WriteLine("");
                    }
                }

            }
        }
    }
}
