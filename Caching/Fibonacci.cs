using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace Caching
{
    public class Fibonacci
    {
        private ICache<int> cache;

        public Fibonacci(ICache<int> cache)
        {
            this.cache = cache;
        }

        //public int GetNthFibonacci(int n)
        //{
        //    if (n < 0) throw new ArgumentException();
        //    var item = cache.Get(n.ToString());
        //    if (item != null)
        //    {
        //        Console.WriteLine($"{item} was taken from cache.");
        //        return (int)item;
        //    }

        //    //object fibItem = null;
        //    if (n == 0 || n == 1) item = n;
        //    //{
        //    //    cache.Set(n.ToString(), n);
        //    //    return n;
        //    //}
        //    item = item ?? GetNthFibonacci(n - 1) + GetNthFibonacci(n - 2);
        //    Console.WriteLine($"{item}");
        //    //var fibonacciItem = GetNthFibonacci(n - 1) + GetNthFibonacci(n - 2);
        //    cache.Set(n.ToString(), item);
        //    return (int)item;
        //}
        public int GetNthFibonacci(int n)
        {
            if (n < 1) throw new ArgumentException();
            var item = cache.Get(n.ToString());

            if (item != default(int))
            {
                Console.WriteLine($"From cache: {item}.");
                return item;
            }

            var fib = new List<int> { 1, 1 };
            for (var i = 2; i <= n; i++)
            {
                fib.Add(fib[i - 2] + fib[i - 1]);
            }
            Console.WriteLine($"{fib[n]}");
            cache.Set(n.ToString(), fib[n]);
            return fib[n];
        }
    }
}
