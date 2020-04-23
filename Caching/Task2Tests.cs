using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var categoryManager = new EntitiesManager(new MemoryCache<IEnumerable<Category>>());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(categoryManager.GetCategories().Count());
            }
        }
    }
}
