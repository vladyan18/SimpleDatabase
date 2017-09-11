using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDatabase
{
    interface IDataLayer
    {
        void Add(Human h);
        void DeleteById(Guid Id);
        void Update(Human h);
        Human FindByName(string Name, string LastName);
        Human FindById(Guid Id);
    }
}
