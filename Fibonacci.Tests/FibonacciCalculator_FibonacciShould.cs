using System;
using Xunit;
using Fibonacci;

namespace Fibonacci.UnitTests
{
    public class FibonacciCalulator_FibonacciShould
    {
        private readonly FibonacciCalculator _fibonacci;

        public FibonacciCalulator_FibonacciShould()
        {
            _fibonacci = new FibonacciCalculator();
        }

        [Fact]
        public void ReturnErrorWhenLowerThanMinimumNegativeFNum()
        {
            long n = -93;
            Exception ex = Assert.Throws<ArgumentException>(() => _fibonacci.NthFibonacciNum(n));
        }

        [Fact]
        public void HaveMinimumNegativeFNum()
        {
            long n = -92;
            long f = _fibonacci.NthFibonacciNum(n);
            Assert.Equal(-7540113804746346429, f);
        }

        [Fact]
        public void ReturnsNegafibonacciSequence()
        {
            long n = -91;
            long f = _fibonacci.NthFibonacciNum(n);
            Assert.Equal(4660046610375530309, f);
        }

        [Fact]
        public void HaveMaximumPositiiveFNum()
        {
            long n = 92;
            long f = _fibonacci.NthFibonacciNum(n);
            Assert.Equal((long) 7540113804746346429, f);
        }

        [Fact]
        public void ReturnErrorWhenGreaterThanMaximumPositiveFNum()
        {
            long n = 93;
            Exception ex = Assert.Throws<ArgumentException>(() => _fibonacci.NthFibonacciNum(n));
        }
        
    }
}
