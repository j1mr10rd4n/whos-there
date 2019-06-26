using System;
using Xunit;
using System.Net.Http;
using System.Text.RegularExpressions;
using Common.FunctionalTests;

namespace Fibonacci.FunctionalTests
{
    public class Fibonacci_NthFibonacciNumEndpointShould : EndpointTest
    {

        private readonly IFibonacci_HttpClient _fibonacci;

        public Fibonacci_NthFibonacciNumEndpointShould() :
            base()
        {
            _fibonacci = new Fibonacci_HttpClient(_client, _host);
        }

        [Fact]
        public async void ReturnError400WhenLowerThanMinimumNegativeFNum()
        {
            int n = -93;
            var (StatusCode, Content) = await _fibonacci.NthFibonacciNum(n);
            Assert.Equal("400", StatusCode);
            Assert.Equal("", Content);
        }

        [Fact]
        public async void HaveMinimumNegativeFNum()
        {
            int n = -92;
            var (StatusCode, Content) = await _fibonacci.NthFibonacciNum(n);
            Assert.Equal("200", StatusCode);
            Assert.Equal("-7540113804746346429", Content);
        }

        [Fact]
        public async void ReturnsNegafibonacciSequence()
        {
            int n = -91;
            var (StatusCode, Content) = await _fibonacci.NthFibonacciNum(n);
            Assert.Equal("200", StatusCode);
            Assert.Equal("4660046610375530309", Content);
        }

        [Fact]
        public async void HaveMaximumPositiiveFNum()
        {
            int n = 92;
            var (StatusCode, Content) = await _fibonacci.NthFibonacciNum(n);
            Assert.Equal("200", StatusCode);
            Assert.Equal("7540113804746346429", Content);
        }

        [Fact]
        public async void ReturnError400WhenGreaterThanMaximumPositiveFNum()
        {
            int n = 93;
            var (StatusCode, Content) = await _fibonacci.NthFibonacciNum(n);
            Assert.Equal("400", StatusCode);
            Assert.Equal("", Content);
        }

        [Fact]
        public async void ReturnErrorMessagGivenString()
        {
            var (StatusCode, Content) = await _fibonacci.NthFibonacciNum("one");
            Assert.Equal("400", StatusCode);
            Assert.Equal(@"{""message"":""The request is invalid.""}", Content);
        }

    }
}
