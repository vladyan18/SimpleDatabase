using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.DataLayer;

namespace Database.Model
{
    public class Table
    {
        private readonly ITableDataController _dataController;
        public readonly string Name;
        private List<Entity> _entities = new List<Entity>();
        private Schema _schema = new Schema();

        public Table(string name)
        {
            Name = name;
            _dataController = new DataLayer.TableDataController();
            _dataController.SetName(name);
        }

        public void Load()
        {
            var t = _dataController.LoadTable();
            _schema = t.Item1;
            _entities = t.Item2;

        }

        public void Save()
        {
            _dataController.SaveTable(_schema, _entities);
        }

        public void AddField(string name, string type)
        {
            _schema.AddField(name, type);
            foreach (var e in _entities)
            {
                e.AddField(name);
            }
            _dataController.SaveTable(_schema, _entities);
        }

        public void DeleteField(string name)
        {

        }

        public Guid Add()
        {
            var e = new Entity(_schema);
            _entities.Add(e);
            _dataController.SaveEntity(e);
            return e.Id;
        }

        public void DeleteById(Guid id)
        {
            _entities.Remove(_entities.Find((x) => x.Id == id));
            _dataController.DeleteEntity(id);
        }

        public void Change(Guid id, Hashtable fields)
        {
            var a = _entities.Find((x) => x.Id.Equals(id));
            foreach (var field in fields.Keys)
            {
                a.Fields[field] = fields[field];
            }

            _dataController.SaveEntity(a);
        }

        public List<Entity> Select(string field, string value)
        {
            return _entities.FindAll((x) => x.Fields[field].Equals(value));
        }

        public List<Entity> Select()
        {
            return _entities;
        }
    }
}
