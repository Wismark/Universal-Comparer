using System;
using System.Collections.Generic;
using System.Globalization;
using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void OwnerChiefExistence()
        {
            //Organization
            var person1 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = null,
                FirstName = "John",
                LastName = "Doe",
                Number = 223
            };

            var person2 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = person1,
                FirstName = "Hikel",
                LastName = "Lobar",
                Number = 100
            };

            var car1 = new Car()
            {
                Cost = 2342.12,
                HasEngine = true,
                MaxSpeed = 100,
                Owner = person1
            };

            var car2 = new Car()
            {
                Cost = 33320.12,
                HasEngine = true,
                MaxSpeed = 100,
                Owner = person2
            };

            string str = "HasEngine, MaxSpeed desc, Owner.Chief.Born.Day";
            var comparer = new UniversalComparerLibrary.UniversalComparer(str, false);
            
            //Action
            int result = comparer.Compare(car1, car2);

            //Assert
            Assert.AreEqual(result, -1);
        }
        [TestMethod]
        public void OwnerChiefExistenceWithNullParamTrue()
        {
            //Organization
            var person1 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = null,
                FirstName = "John",
                LastName = "Doe",
                Number = 223
            };

            var person2 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = person1,
                FirstName = "Hikel",
                LastName = "Lobar",
                Number = 100
            };

            var car1 = new Car()
            {
                Cost = 2342.12,
                HasEngine = true,
                MaxSpeed = 100,
                Owner = person1
            };

            var car2 = new Car()
            {
                Cost = 33320.12,
                HasEngine = true,
                MaxSpeed = 100,
                Owner = person2
            };

            string str = "HasEngine, MaxSpeed desc, Owner.Chief.Born.Day";
            var comparer = new UniversalComparerLibrary.UniversalComparer(str, true);

            //Action
            int result = comparer.Compare(car1, car2);

            //Assert
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void EqualCars()
        {
            //Organization
            var person1 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = null,
                FirstName = "John",
                LastName = "Doe",
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

            var car1 = new Car()
            {
                Cost = 2342.12,
                HasEngine = true,
                MaxSpeed = 100,
                Owner = person1
            };

            var car2 = new Car()
            {
                Cost = 33320.12,
                HasEngine = true,
                MaxSpeed = 100,
                Owner = person2
            };

            string str = "HasEngine, MaxSpeed desc, Owner.Chief.Born.Day";
            var comparer = new UniversalComparerLibrary.UniversalComparer(str, false);

            //Action
            int result = comparer.Compare(car1, car2);

            //Assert
            Assert.AreEqual(result, 0);
        }


        [TestMethod]
        public void CompareLastName()
        {
            //Organization
            var person1 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = null,
                FirstName = "John",
                LastName = "Aoren",
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

            string str = "LastName, FirstName, Born.Year desc";
            var comparer = new UniversalComparerLibrary.UniversalComparer(str, false);

            //Action
            int result = comparer.Compare(person1, person2);

            //Assert
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void CompareChiefBirthdaysByDesc()
        {
            var chief1 = new Person()
            {
                Born = DateTime.MaxValue,
            };

            var chief2 = new Person()
            {
                Born = DateTime.MinValue,
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
                Chief = chief2,
                FirstName = "Hikel",
                LastName = "Lobar",
                Number = 100
            };

            string str = "FirstName desc, Born.Day, Chief.Born.Day desc";
            var comparer = new UniversalComparerLibrary.UniversalComparer(str, false);

            //Action
            int result = comparer.Compare(person1, person2);

            //Assert
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void CompareChiefBirthdaysWithNullChief()
        {
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

            string str = "FirstName desc, Born.Day, Chief.Born.Day";
            var comparer = new UniversalComparerLibrary.UniversalComparer(str, false);

            //Action
            int result = comparer.Compare(person1, person2);

            //Assert
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void CompareChiefBirthdaysWithNullChiefWithNullParamTrue()
        {
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
            var comparer = new UniversalComparerLibrary.UniversalComparer(str, true);

            //Action
            int result = comparer.Compare(person1, person2);

            //Assert
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void DayMonthSort()
        {
            //Organization

            var test = new List<Person>();
            test.AddRange(new[]
            {
                new Person() {FirstName = "Ketty", Born=DateTime.ParseExact(  "2009-05-08", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
                new Person() {FirstName = "Mikel", Born=DateTime.ParseExact(  "2009-05-07", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
                new Person() {FirstName = "Nicole", Born=DateTime.ParseExact( "2009-09-06", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
                new Person() {FirstName = "Ben", Born=DateTime.ParseExact(    "2009-05-06", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
                new Person() {FirstName = "Anton", Born=DateTime.ParseExact(  "2009-02-01", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
                new Person() {FirstName = "Michiel", Born=DateTime.ParseExact("2009-02-06", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
                new Person() {FirstName = "Lovar", Born=DateTime.ParseExact(  "2009-05-03", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
                new Person() {FirstName = "Lariot", Born=DateTime.ParseExact( "2009-05-12", "yyyy-MM-dd", CultureInfo.InvariantCulture)},
            });

            string str = "Born.Day desc, Born.Month desc"; //Born.Day desc, Born.Month desc

            var comparer = new UniversalComparerLibrary.UniversalComparer(str, true);           
            
            //Action
            test.Sort(comparer);

            //Assert
            Assert.AreEqual(test[0].FirstName, "Lariot");
            Assert.AreEqual(test[test.Count-1].FirstName, "Anton");

        }
    }
}
