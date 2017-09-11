using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public class Schema
    {
        public Hashtable Types = new Hashtable();

        public void AddField(string name , string type )
        {
            Types.Add(name, type);
        }
    }
}
