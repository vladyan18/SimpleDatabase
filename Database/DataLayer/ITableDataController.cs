using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Model;

namespace Database.DataLayer
{
    public interface ITableDataController
    {
        void SaveEntity(Entity e);
        void SaveTable(Schema schema, List<Entity> table);
        void SetName(string name);
        void DeleteEntity(Guid id);
        Tuple<Model.Schema, List<Entity>> LoadTable();
    }
}
