using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Database;
using Database.Model;

namespace Database.DataLayer
{
    public class TableDataController: ITableDataController
    {
        private string curDir = Directory.GetCurrentDirectory();
        private string _path;

        public void SetName(string name)
        {
            _path = name;
        }

        public void SaveEntity(Entity e)
        {
            File.WriteAllText(_path + "/" + e.Id.ToString() + ".json", JsonConvert.SerializeObject(e));
        }

        public void SaveTable(Schema schema, List<Entity> table)
        {
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
            if (!Directory.Exists(_path + "/schema/")) Directory.CreateDirectory(_path + "/schema/");
            foreach (var e in table)
            {
                SaveEntity(e);
            }
            File.WriteAllText(_path + "/schema/schema.json", JsonConvert.SerializeObject(schema));
        }

        public void DeleteEntity(Guid id)
        {
            File.Delete(_path + id.ToString() + ".json");
        }

        public Tuple<Model.Schema, List<Entity>> LoadTable()
        {
            var entityNames = Directory.GetFiles(_path, "*.json");

            List<Entity> entities = new List<Entity>();
            foreach (var name in entityNames)
            {
                entities.Add(JsonConvert.DeserializeObject<Entity>(File.ReadAllText(name)));
            }
            return new Tuple<Schema, List<Entity>>(JsonConvert.DeserializeObject<Schema>(File.ReadAllText(_path + "/schema/schema.json")), entities);
        }
    }
}
