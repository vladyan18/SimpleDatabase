using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDatabase
{
    public class Human
    {
        public string Name;
        public string LastName;
        public int Age;
        public int Weight;
        public string FavouriteMusicBand;
        public Guid Id = Guid.NewGuid();

        public void CopyTo(Human h)
        {
            h.Name = Name;
            h.LastName = LastName;
            h.Age = Age;
            h.Weight = Weight;
            h.FavouriteMusicBand = FavouriteMusicBand;
            h.Id = Id;
        }
    }
}
