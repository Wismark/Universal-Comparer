using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniversalComparer;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
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
                number = 223
            };

            var person2 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = person1,
                FirstName = "Hikel",
                LastName = "Lobar",
                number = 100
            };

            var car1 = new Car()
            {
                Cost = 2342.12,
                HasEngine = true,
                maxSpeed = 100,
                Owner = person1
            };

            var car2 = new Car()
            {
                Cost = 33320.12,
                HasEngine = true,
                maxSpeed = 100,
                Owner = person2
            };

            string str = "HasEngine, maxSpeed desc, Owner.Chief.Born.Day";
            var comparer = new UniversalComparer.UniversalComparer(str, false);
            
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
                number = 223
            };

            var person2 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = person1,
                FirstName = "Hikel",
                LastName = "Lobar",
                number = 100
            };

            var car1 = new Car()
            {
                Cost = 2342.12,
                HasEngine = true,
                maxSpeed = 100,
                Owner = person1
            };

            var car2 = new Car()
            {
                Cost = 33320.12,
                HasEngine = true,
                maxSpeed = 100,
                Owner = person2
            };

            string str = "HasEngine, maxSpeed desc, Owner.Chief.Born.Day";
            var comparer = new UniversalComparer.UniversalComparer(str, true);

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
                number = 223
            };

            var person2 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = null,
                FirstName = "Hikel",
                LastName = "Lobar",
                number = 100
            };

            var car1 = new Car()
            {
                Cost = 2342.12,
                HasEngine = true,
                maxSpeed = 100,
                Owner = person1
            };

            var car2 = new Car()
            {
                Cost = 33320.12,
                HasEngine = true,
                maxSpeed = 100,
                Owner = person2
            };

            string str = "HasEngine, maxSpeed desc, Owner.Chief.Born.Day";
            var comparer = new UniversalComparer.UniversalComparer(str, false);

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
                number = 223
            };

            var person2 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = null,
                FirstName = "Hikel",
                LastName = "Lobar",
                number = 100
            };

            string str = "LastName, FirstName, Born.Year desc";
            var comparer = new UniversalComparer.UniversalComparer(str, false);

            //Action
            int result = comparer.Compare(person1, person2);

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
                number = 223
            };

            var person2 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = chief2,
                FirstName = "Hikel",
                LastName = "Lobar",
                number = 100
            };

            string str = "FirstName desc, Born.Day, Chief.Born.Day desc";
            var comparer = new UniversalComparer.UniversalComparer(str, false);

            //Action
            int result = comparer.Compare(person1, person2);

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
                number = 223
            };

            var person2 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = null,
                FirstName = "Hikel",
                LastName = "Lobar",
                number = 100
            };

            string str = "FirstName desc, Born.Day, Chief.Born.Day";
            var comparer = new UniversalComparer.UniversalComparer(str, false);

            //Action
            int result = comparer.Compare(person1, person2);

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
                number = 223
            };

            var person2 = new Person()
            {
                Born = DateTime.MinValue,
                Chief = null,
                FirstName = "Hikel",
                LastName = "Lobar",
                number = 100
            };

            string str = "FirstName desc, Born.Day, Chief.Born.Day desc";
            var comparer = new UniversalComparer.UniversalComparer(str, true);

            //Action
            int result = comparer.Compare(person1, person2);

            Assert.AreEqual(result, 1);
        }
    }
}
