using System;
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
                Born = DateTime.MinValue,
                Chief = person1,
                FirstName = "Hikel",
                LastName = "Lobar",
                number = 1
            };
            people.AddRange(new Person[] { person1, person2, person3 });

            string t1 = "Chief.Born.Day, FirstName, Born.Year desc, number",
                t2 = "FirstName desc, Born.Day, Chief.Born.Day desc",
                t4 = "LastName, FirstName, number", //"FirstName desc, desc, Chief.Born.Day desc, descot desc";
                t5 = "FirstName, number";

            //ShowCollection(people);
            var comparer = new UniversalComparer(t1, true);
            //people.Sort(0,3, comparer as IComparer<Person>);
            //ShowCollection(people);

            Console.WriteLine("--------");
            Console.WriteLine("result of compare:"+ comparer.Test2(person3, person4));
          //  Console.WriteLine(comparer.Compare("Hikel", "Hikel"));
            Console.ReadLine();
        }

        public static void ShowCollection(List<Person> peopleList)
        {
            Console.WriteLine("People:");
            foreach (var person in peopleList)
            {
                Console.WriteLine(String.Concat($"Name:{person.FirstName} {person.LastName}  Born:{person.Born.Date.ToString("MM/dd/yyyy",CultureInfo.InvariantCulture)} "
                                 ,(person.Chief is null ? "" : "Chief:"+ person.Chief.FirstName + " " + person.Chief.LastName)));
            }
        }
    }
}
