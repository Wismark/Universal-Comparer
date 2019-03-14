using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniversalComparer;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
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
        public void TestMethod2()
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
        public void TestMethod3()
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

            //AssertAssert.AreEqual(result, -1);
        }
    }
}
