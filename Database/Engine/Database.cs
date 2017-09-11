using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Database.DataLayer;
using Database.Model;
using Database.WebApi;
using NLog;

namespace Database.Engine
{
    public class Database
    {
        public string Name;
        Hashtable Tables = new Hashtable();
        DatabaseDataController _dataController = new DatabaseDataController();
        WebApi.Server _api;

        public Database()
        {
            _api = new Server(this);
        }

        public void Open(string name)
        {
            Tables.Clear();
            Name = name;
            var tableNames = _dataController.OpenDatabase(name);
            foreach (var tableName in tableNames)
            {
                var tb = new Table(tableName);
                tb.Load();
                Tables.Add(tableName.Split('/').Last().Split('.').First(), tb);
            }
        }

        public void Create(string name)
        {
            Name = name;
            _dataController.CreateDatabase(name);
        }

        public void Run()
        {
            _api.Run();
        }

        public Tuple<string, object> DoCommand(string command)
        {
            var words = command.Split(' ');
            try
            {
                switch (words[0])
                {
                    case "CREATE":
                        Tables.Add(words[1],
                            new Table(Directory.GetCurrentDirectory() + "/" + Name + "/" + words[1] + "/"));
                        ((Table) Tables[words[1]]).Save();
                        return new Tuple<string, object>("OK", null);
                        break;

                    case "ADDFIELD":
                        ((Table) Tables[words[1]]).AddField(words[2], words[3]);
                        return new Tuple<string, object>("OK", null);
                        break;

                    case "DELETEFIELD":
                        ((Table) Tables[words[1]]).DeleteField(words[2]);
                        return new Tuple<string, object>("OK", null);
                        break;

                    case "ADD":
                        var guid = ((Table) Tables[words[1]]).Add();
                        return new Tuple<string, object>("ADD", guid);
                        break;

                    case "DELETE":
                        break;

                    case "SELECT":
                        if (words.Length == 2)
                        {
                            return new Tuple<string, object>("SELECT", ((Table) Tables[words[1]]).Select());
                        }
                        if (words[2] == "WHERE")
                        {
                            return new Tuple<string, object>("SELECT",
                                ((Table) Tables[words[1]]).Select(words[3], words[4]));
                        }
                        break;

                    case "CHANGE":
                        var id = Guid.Parse(words[2]);
                        var values = new Hashtable();
                        for (int i = 3; i < words.Length; i += 2)
                        {
                            values.Add(words[i], words[i + 1]);
                        }
                        ((Table) Tables[words[1]]).Change(id, values);
                        return new Tuple<string, object>("OK", null);
                        break;
                }
            }
            catch (ArgumentException e)
            {
                return new Tuple<string, object>("ERROR", "Invalid argument");
            }
            catch (IndexOutOfRangeException e)
            {
                return new Tuple<string, object>("ERROR", "Argument exception");
            }
            catch (NullReferenceException e)
            {
                return new Tuple<string, object>("ERROR", "Not found");
            }
            return new Tuple<string, object>("ERROR", "Invalid command");
        }

    }
}
