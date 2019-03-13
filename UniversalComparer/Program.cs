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
                Born = DateTime.MinValue,
                Chief = person1,
                FirstName = "Donald",
                LastName = "Duck",
                number = 223
    };
            var person3 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = person1,
                FirstName = "Mike",
                LastName = "Lobar",
                number = 213
            };

            var person4 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = person1,
                FirstName = "Mike",
                LastName = "Lobar",
                number = 222
            };
            people.AddRange(new Person[] { person1, person2, person3 });

            string t1 = "LastName, FirstName, Born.Year desc", t2 = "FirstName desc, Born.Day, Chief.Born.Day desc",
            t3 = "FirstName desc, Desc, Chief.Born.Day desc, descot desc", t4 = "LastName, FirstName, number"; //"FirstName desc, desc, Chief.Born.Day desc, descot desc";


            //ShowCollection(people);
            var comparer = new UniversalComparer(t1);
            //people.Sort(0,3, comparer as IComparer<Person>);
            //ShowCollection(people);

            Console.WriteLine("--------");
            Console.WriteLine(comparer.Test(person3, person4));
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
        public class Person
        {
            public Person Chief;
            public String FirstName;
            public String LastName;
            public DateTime Born;
            public int number;
        }

    }
}
