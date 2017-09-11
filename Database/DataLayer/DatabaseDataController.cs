using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DataLayer
{
    class DatabaseDataController
    {
        private string curDir = Directory.GetCurrentDirectory();
        public void CreateDatabase(string name)
        {
            Directory.CreateDirectory(curDir + "/" + name + "/");
        }

        public List<string> OpenDatabase(string name)
        {
            return Directory.GetDirectories(curDir + "/" + name + "/").ToList();
        }
    }
}
