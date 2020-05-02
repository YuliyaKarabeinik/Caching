using System;
using System.Collections.Generic;
using Infrastructure;

namespace Caching
{
    public class Fibonacci
    {
        private ICache cache;

        public Fibonacci(ICache cache)
        {
            this.cache = cache;
        }

        public int GetNthFibonacci(int n)
        {
            if (n < 1) throw new ArgumentException();
            var item = cache.Get<int>(n.ToString());

            if (item != default)
            {
                Console.WriteLine($"From cache: {item}");
                return item;
            }

            var fib = new List<int> { 1, 1 };
            for (var i = 2; i <= n; i++)
            {
                fib.Add(fib[i - 2] + fib[i - 1]);
            }
            Console.WriteLine($"{fib[n]}");
            cache.Set(n.ToString(), fib[n], DateTime.Now.AddSeconds(60));
            return fib[n];
        }
    }
}
