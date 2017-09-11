using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace SimpleDatabase
{
    class DataLayer: IDataLayer
    {
        private string _databaseName;
        private List<Human> Database = new List<Human>();

        public void CreateDatabase(string Name)
        {
            _databaseName = Name;
            Database = new List<Human>();
            SaveDatabase();
        }

        public void OpenDatabase(string Name)
        {
            _databaseName = Name;
            Database.Clear();
           Database = JsonConvert.DeserializeObject<List<Human>>(File.ReadAllText(Name + ".json"));
        }

        public void Add(Human h)
        {
            Database.Add(h);
            SaveDatabase();            
        }

        public void DeleteById(Guid Id)
        {
            Database.Remove(Database.Find((x) => x.Id == Id));
            SaveDatabase();
        }

        public void Update(Human h)
        {
            var a = Database.Find((x) => x.Id.Equals(h.Id));
            h.CopyTo(a);
            SaveDatabase();
        }

        public Human FindByName(string Name, string LastName)
        {
            return Database.Find((x) => x.Name.Equals(Name) && x.LastName.Equals(LastName));
        }

        public Human FindById(Guid Id)
        {
            return Database.Find((x) => x.Id.Equals(Id));
        }

        private void SaveDatabase()
        {
            File.WriteAllText(_databaseName + ".json", JsonConvert.SerializeObject(Database));
        }
    }
}
