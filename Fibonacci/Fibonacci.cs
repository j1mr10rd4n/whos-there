using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Fibonacci
{

    public interface IFibonacciCalculator
    {
        long NthFibonacciNum(long n);
    }

    public class FibonacciCalculator : IFibonacciCalculator
    {
        private static long MIN = -92;
        private static long MAX = 92;
        private static IDictionary<long, long> _cache = new Dictionary<long, long>()
        {
            { 0, 0},
            { 1, 1},
        };

        static FibonacciCalculator()
        {
            for (var i = 2; i <= MAX; i++)
            {
                _cache.TryGetValue(i-2, out long nMinusTwo);
                _cache.TryGetValue(i-1, out long nMinusOne);
                _cache.Add(i, (nMinusTwo + nMinusOne));
            }
            for (var i = -1; i >= MIN; i--)
            {
                _cache.TryGetValue(i+2, out long nPlusTwo);
                _cache.TryGetValue(i+1, out long nPlusOne);
                _cache.Add(i, (nPlusTwo - nPlusOne));
            }
        }

        public long NthFibonacciNum(long n)
        {
            if (_cache.TryGetValue(n, out long f))
            {
                return f;
            } else {
                throw new ArgumentException();
            }
        }
    }
}

