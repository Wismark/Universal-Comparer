using System;
using System.Collections.Generic;
using System.Globalization;
using ClassLibrary;

namespace UniversalComparer
{
    internal class Program
    {
        private static void Main()
        {
           // UniversalComparer a =new UniversalComparer("Chief.Born.Day, FirstName, Born.Year desc, number", false); //a.ParseSortConditionReborn();
           // var people = new List<Person>();

            //var person1 = new Person
            //{
            //    Born = DateTime.MinValue,
            //    Chief = null,
            //    FirstName = "John",
            //    LastName = "Doe",
            //    number = 223
            //};
            //var person2 = new Person
            //{
            //    Born = DateTime.MaxValue,
            //    Chief = person1,
            //    FirstName = "Donald",
            //    LastName = "Duck",
            //    number = 223
            //};
            //var person3 = new Person
            //{
            //    Born = DateTime.ParseExact("02-08-1992", "dd-MM-yyyy", CultureInfo.InvariantCulture),
            //    Chief = null,
            //    FirstName = "Hikel",
            //    LastName = "Lobar",
            //    number = 100
            //};

            //var person4 = new Person
            //{
            //    Born = DateTime.ParseExact("02-08-1996", "dd-MM-yyyy", CultureInfo.InvariantCulture),
            //    Chief = person3,
            //    FirstName = "Aikel",
            //    LastName = "Lobar",
            //    number = 101
            //};

            //var person5 = new Person
            //{
            //    Born = DateTime.ParseExact("06-08-1995", "dd-MM-yyyy", CultureInfo.InvariantCulture),
            //    Chief = person3,
            //    FirstName = "Aikel",
            //    LastName = "Lobar",
            //    number = 101
            //};

            //var car1 = new Car
            //{
            //    Cost = 2342.12,
            //    HasEngine = true,
            //    maxSpeed = 100,
            //    Owner = person4
            //};

            //var car2 = new Car
            //{
            //    Cost = 33320.12,
            //    HasEngine = true,
            //    maxSpeed = 100,
            //    Owner = person1
            //};

            var chief1 = new Person()
            {
                Born = DateTime.MaxValue,
            };
            //Organization
            var person1 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = chief1,
                FirstName = "Hikel",
                LastName = "Lobar",
                Number = 223
            };

            var person2 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = null,
                FirstName = "Hikel",
                LastName = "Lobar",
                Number = 100
            };

            string str = "FirstName desc, Born.Day, Chief.Born.Day desc";
            var comparer = new UniversalComparer.UniversalComparer(str, true);

            //Action
            Console.WriteLine(comparer.Compare(person1, person2));

            //Person[] people2 =
            //{
            //    new Person() {FirstName = "Ketty", Born=DateTime.ParseExact(  "2009-05-08", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
            //    new Person() {FirstName = "Mikel", Born=DateTime.ParseExact(  "2009-05-07", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
            //    new Person() {FirstName = "Nicole", Born=DateTime.ParseExact( "2009-09-06", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
            //    new Person() {FirstName = "Ben", Born=DateTime.ParseExact(    "2009-05-06", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
            //    new Person() {FirstName = "Anton", Born=DateTime.ParseExact(  "2009-02-01", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
            //    new Person() {FirstName = "Michiel", Born=DateTime.ParseExact("2009-02-06", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
            //    new Person() {FirstName = "Lovar", Born=DateTime.ParseExact(  "2009-05-03", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
            //    new Person() {FirstName = "Lariot", Born=DateTime.ParseExact( "2009-05-12", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
            //};

            //string test1 = "Born.Day desc,Born.Month desc";
            //var comparerTest = new UniversalComparer(test1, true);
            //people.AddRange(new[] { person1, person2, person3, person4, person5 });
            //people.AddRange(people2);
            //string t1 = "Chief.Born.Day, FirstName, Born.Year desc, number",
            //    t2 = "FirstName desc, Born.Day, Chief.Born.Day desc",
            //    t4 = "LastName, FirstName, number", 
            //    t5 = "FirstName, number"
            //var str = "HasEngine, maxSpeed desc, Owner.Chief.Born.Day";
            //var str2 = "Owner.Chief.Born.Day, maxSpeed, Owner.Born.Day";
            //ShowCollection(people);
            //Console.WriteLine("--------");
            //people.Sort(comparerTest);
            //ShowCollection(people);
            //Console.WriteLine("--------");
            //Console.WriteLine("result of car compare:" + comparer2.Test2(car1, car2));
            //Console.WriteLine("result of compare:" + comparerTest.Compare(person5, person4));
            Console.ReadLine();
        }

        public static void ShowCollection(List<Person> peopleList)
        {
            Console.WriteLine("People:");
            foreach (var person in peopleList)
                Console.WriteLine(string.Concat(
                    $"Name:{person.FirstName} {person.LastName}  Born:{person.Born.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)} "
                    , (person.Chief is null ? "" : "Chief:" + person.Chief.FirstName + " " + person.Chief.LastName) + " " + person.Number));
        }
    }
}