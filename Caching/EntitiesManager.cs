using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using NorthwindLibrary;
using Infrastructure;

namespace Caching
{
    public class EntitiesManager
    {
        private ICache cache;

        public EntitiesManager(ICache cache)
        {
            this.cache = cache;
        }
        
        public IEnumerable<T> GetEntities<T>() where T : class
        {
            var key = typeof(T).ToString();

            var entities = (List<T>)cache.Get(key);

            if (entities != null)
            {
                Console.WriteLine("From cache:");
                return entities;
            }
            Console.WriteLine("From DB:");
            using (var dbContext = new Northwind())
            {
                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;
                entities = dbContext.Set<T>().ToList();
            }
            cache.Set(key, entities);
            return entities;
        }
    }
}