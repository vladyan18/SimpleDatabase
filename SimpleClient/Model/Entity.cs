using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClient.Model
{
    public class Entity
    {
        public Hashtable Fields = new Hashtable();
        public readonly Guid Id = Guid.NewGuid();

        public void AddField(string name)
        {
            Fields.Add(name, "");
        }


        public Entity()
        {

        }
    }
}
