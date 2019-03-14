using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalComparer
{
    class Program
    {
        static void Main(string[] args)
        {
            var people = new List<Person>();
            var person1 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = null,
                FirstName = "John",
                LastName = "Doe",
                number = 223
            };
            var person2 = new Person()
            {
                Born = DateTime.MaxValue,
                Chief = person1,
                FirstName = "Donald",
                LastName = "Duck",
                number = 223
            };
            //var person1 = new Person()
            //{
            //    Born = DateTime.MinValue,
            //    Chief = null,
            //    FirstName = "John",
            //    LastName = "Aoren",
            //    number = 223
            //};

            //var person2 = new Person()
            //{
            //    Born = DateTime.MinValue,
            //    Chief = null,
            //    FirstName = "Hikel",
            //    LastName = "Lobar",
            //    number = 100
            //};
            var person3 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = person1,
                FirstName = "Hikel",
                LastName = "Lobar",
                number = 100
            };

            var person4 = new Person()
            {
                Born = DateTime.MaxValue,
                Chief = null,
                FirstName = "Aikel",
                LastName = "Lobar",
                number = 100
            };

            var person5 = new Person()
            {
                Born = DateTime.MaxValue,
                Chief = null,
                FirstName = "Aikel",
                LastName = "Lobar",
                number = 999
            };

            var car1 = new Car()
            {
                Cost = 2342.12,
                HasEngine = true,
                maxSpeed = 100,
                Owner = person4
            };

            var car2 = new Car()
            {
                Cost = 33320.12,
                HasEngine = true,
                maxSpeed = 100,
                Owner = person1
            };

            people.AddRange(new Person[] { person1, person2, person3, person4, person5 });

            string t1 = "Chief.Born.Day, FirstName, Born.Year desc, number",
                t2 = "FirstName desc, Born.Day, Chief.Born.Day desc",
                t4 = "LastName, FirstName, number", //"FirstName desc, desc, Chief.Born.Day desc, descot desc";
                t5 = "FirstName, number";

            string str = "HasEngine, maxSpeed desc, Owner.Chief.Born.Day";
            string str2 = "Owner.Chief.Born.Day, maxSpeed, Owner.Born.Day";
            ShowCollection(people);
            //Console.ReadKey();
            var comparer = new UniversalComparer("LastName, FirstName, Born.Year desc", false);
            var comparer2 = new UniversalComparer(str2, false);
            Console.WriteLine("--------");
            people.Sort(comparer);
            ShowCollection(people);
            //  Console.WriteLine(comparer.Compare(null, null));
            //  Console.WriteLine("--------");
            //  Console.WriteLine("result of car compare:" + comparer2.Test2(car1, car2));
            //  Console.WriteLine("result of compare:"+ comparer.Test2(person1, person2));
            //  Console.WriteLine(Comparer.Default.Compare("Aoren", "Lobar"));
            Console.ReadLine();
        }

        public static void ShowCollection(List<Person> peopleList)
        {
            Console.WriteLine("People:");
            foreach (var person in peopleList)
            {
                Console.WriteLine(String.Concat($"Name:{person.FirstName} {person.LastName}  Born:{person.Born.Date.ToString("MM/dd/yyyy",CultureInfo.InvariantCulture)} "
                                 ,(person.Chief is null ? "" : "Chief:"+ person.Chief.FirstName + " " + person.Chief.LastName ) + " " + person.number));
            }
        }
    }
}
