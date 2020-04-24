using System;
using System.Linq;
using System.Threading;
using Infrastructure;
using NorthwindLibrary;
using NUnit.Framework;

namespace Caching
{
    [TestFixture]
    class Task2Tests
    {
        [Test]
        public void MemoryCache()
        {
            var entitiesManager = new EntitiesManager(new MemoryCache());

            for (var i = 0; i < 3; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities<Product>().Count());
                Console.WriteLine(entitiesManager.GetEntities<Order>().Count());
                Console.WriteLine(entitiesManager.GetEntities<Employee>().Count());

                Thread.Sleep(100);
            }
        }

        [Test]
        public void RedisCache()
        {
            var entitiesManager = new EntitiesManager(new RedisCache("localhost"));

            for (var i = 0; i < 3; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities<Customer>().Count());
                Console.WriteLine(entitiesManager.GetEntities<Category>().Count());
                Console.WriteLine(entitiesManager.GetEntities<Shipper>().Count());

                Thread.Sleep(100);
            }
        }
    }
}
