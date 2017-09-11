using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDatabase
{
    class Program
    {
        private static DataLayer _database = new DataLayer(); 

        static void Main(string[] args)
        {
            System.Console.Out.WriteLine("1. Create new database." + "\n" + "2. Open database.");
            var c = Int16.Parse(System.Console.In.ReadLine());
            switch (c)
            {
                case 1:
                    createDatabase();
                    break;
                case 2:
                    openDatabase();
                    break;
                default:
                    return;
            }

            while (c != 0)
            {
                System.Console.Out.WriteLine();
                System.Console.Out.WriteLine("0. Exit.");
                System.Console.Out.WriteLine("1. Add new record.");
                System.Console.Out.WriteLine("2. Find record by name and LastName.");
                System.Console.Out.WriteLine("3. Find record by Id.");
                c = Int16.Parse(System.Console.In.ReadLine());
                switch (c)
                {
                    case 1:
                        addRecord();
                        break;
                    case 2:
                        findByName();
                        break;
                    case 3:
                        findById();
                        break;
                    default:
                        return;
                }
            }

        }

        static void createDatabase()
        {
            System.Console.Out.WriteLine("Name for the new database: ");
            var name = System.Console.In.ReadLine();
            _database.CreateDatabase(name);


        }

        static void openDatabase()
        {
            System.Console.Out.WriteLine("Name of the existing database: ");
            var name = System.Console.In.ReadLine();
            _database.OpenDatabase(name);
        }

        private static void findById()
        {
            System.Console.Out.WriteLine("Id: ");
            var id = Guid.Parse(System.Console.In.ReadLine());
            var h = _database.FindById(id);

            System.Console.Out.WriteLine("Name: " + h.Name);
            System.Console.Out.WriteLine("LastName: " + h.LastName);
            System.Console.Out.WriteLine("Age: " + h.Age.ToString());
            System.Console.Out.WriteLine("Weight: " + h.Weight.ToString());
            System.Console.Out.WriteLine("Favourite Music Band: " + h.FavouriteMusicBand);
            System.Console.Out.WriteLine("Id: " + h.Id.ToString());

            System.Console.Out.WriteLine();
            System.Console.Out.WriteLine("0. Return.");
            System.Console.Out.WriteLine("1. Change.");
            System.Console.Out.WriteLine("2. Delete.");
            var c = Int16.Parse(System.Console.In.ReadLine());
            switch (c)
            {
                case 0:
                    break;
                case 1:
                    System.Console.Out.WriteLine("Name: ");
                    h.Name = System.Console.In.ReadLine();
                    System.Console.Out.WriteLine("LastName: ");
                    h.LastName = System.Console.In.ReadLine();
                    System.Console.Out.WriteLine("Age: ");
                    h.Age = Int16.Parse(System.Console.In.ReadLine());
                    System.Console.Out.WriteLine("Weight: ");
                    h.Weight = Int16.Parse(System.Console.In.ReadLine());
                    System.Console.Out.WriteLine("Favourite Music Band: ");
                    h.FavouriteMusicBand = System.Console.In.ReadLine();
                    _database.Update(h);
                    break;
                case 2:
                    _database.DeleteById(h.Id);
                    break;
                default:
                    return;
            }
        }

        private static void findByName()
        {
            System.Console.Out.WriteLine("Name: ");
            var name = System.Console.In.ReadLine();
            System.Console.Out.WriteLine("LastName: ");
            var lastName = System.Console.In.ReadLine();
            var h = _database.FindByName(name, lastName);
            System.Console.Out.WriteLine();
            if (h == null)
            { System.Console.Out.WriteLine("Not found."); }
            else
            {
                System.Console.Out.WriteLine("Name: " + h.Name);
                System.Console.Out.WriteLine("LastName: " + h.LastName);
                System.Console.Out.WriteLine("Age: " + h.Age.ToString());
                System.Console.Out.WriteLine("Weight: " + h.Weight.ToString());
                System.Console.Out.WriteLine("Favourite Music Band: " + h.FavouriteMusicBand);
                System.Console.Out.WriteLine("Id: " + h.Id.ToString());

                System.Console.Out.WriteLine();
                System.Console.Out.WriteLine("0. Return.");
                System.Console.Out.WriteLine("1. Change.");
                System.Console.Out.WriteLine("2. Delete.");
                var c = Int16.Parse(System.Console.In.ReadLine());
                switch (c)
                {
                    case 0:
                        break;
                    case 1:
                        System.Console.Out.WriteLine("Name: ");
                        h.Name = System.Console.In.ReadLine();
                        System.Console.Out.WriteLine("LastName: ");
                        h.LastName = System.Console.In.ReadLine();
                        System.Console.Out.WriteLine("Age: ");
                        h.Age = Int16.Parse(System.Console.In.ReadLine());
                        System.Console.Out.WriteLine("Weight: ");
                        h.Weight = Int16.Parse(System.Console.In.ReadLine());
                        System.Console.Out.WriteLine("Favourite Music Band: ");
                        h.FavouriteMusicBand = System.Console.In.ReadLine();
                        _database.Update(h);
                        break;
                    case 2:
                        _database.DeleteById(h.Id);
                        break;
                    default:
                        return;
                }
            }

        }

        private static void addRecord()
        {
            Human h = new Human();
            System.Console.Out.WriteLine("Name: ");
            h.Name = System.Console.In.ReadLine();
            System.Console.Out.WriteLine("LastName: ");
            h.LastName = System.Console.In.ReadLine();
            System.Console.Out.WriteLine("Age: ");
            h.Age = Int16.Parse(System.Console.In.ReadLine());
            System.Console.Out.WriteLine("Weight: ");
            h.Weight = Int16.Parse(System.Console.In.ReadLine());
            System.Console.Out.WriteLine("Favourite Music Band: ");
            h.FavouriteMusicBand = System.Console.In.ReadLine();
            System.Console.Out.WriteLine("Id: ");
            System.Console.Out.WriteLine(h.Id.ToString());
            _database.Add(h);
        }
    }
}
