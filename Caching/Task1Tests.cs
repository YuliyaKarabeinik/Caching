using System;
using Infrastructure;
using NUnit.Framework;

namespace Caching
{
    [TestFixture]
    public class Task1Tests
    {

        [Test]
        public void MemoryCache()
        {
            var fib = new Fibonacci(new MemoryCache<int>());
            fib.GetNthFibonacci(3);
            fib.GetNthFibonacci(3);
            fib.GetNthFibonacci(10);
            fib.GetNthFibonacci(2);
            fib.GetNthFibonacci(1);
            fib.GetNthFibonacci(1);
            fib.GetNthFibonacci(2);
        }

        [Test]
        public void RedisCache()
        {
            var fib = new Fibonacci(new RedisCache<int>("localhost"));
            fib.GetNthFibonacci(3);
            fib.GetNthFibonacci(1);
            fib.GetNthFibonacci(3);
            fib.GetNthFibonacci(1);
        }
    }
}
