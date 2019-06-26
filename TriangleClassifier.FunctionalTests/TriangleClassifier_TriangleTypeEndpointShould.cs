using System;
using Xunit;
using System.Net.Http;
using System.Text.RegularExpressions;
using Common.FunctionalTests;

namespace TriangleClassifier.FunctionalTests
{
    public class TriangleClassifier_TriangleTypeEndpointShould : EndpointTest
    {

        private readonly ITriangleClassifier_HttpClient _triangleClassifier;

        public TriangleClassifier_TriangleTypeEndpointShould() :
            base()
        {
            _triangleClassifier = new TriangleClassifier_HttpClient(_client, _host);
        }

        [Fact]
        public async void ClassifiyAnEquilateralTriangle()
        {
            var (StatusCode, Content) = await _triangleClassifier.TriangleType(3, 3, 3);
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"Equilateral\"", Content);
        }

        [Fact]
        public async void ClassifiyAScaleneTriangle()
        {
            var (StatusCode, Content) = await _triangleClassifier.TriangleType(3, 4, 5);
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"Scalene\"", Content);
        }

        [Fact]
        public async void ClassifiyAnIsoscelesTriangle()
        {
            var (StatusCode, Content) = await _triangleClassifier.TriangleType(3, 3, 5);
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"Isosceles\"", Content);
        }

        [Fact]
        public async void ReturnTheStringErrorWhenGivenAZero()
        {
            var (StatusCode, Content) = await _triangleClassifier.TriangleType(3, 3, 0);
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"Error\"", Content);
        }

        [Fact]
        public async void ReturnTheStringErrorWhenGivenAnImpossibleTriangle()
        {
            var (StatusCode, Content) = await _triangleClassifier.TriangleType(1, 1, 2);
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"Error\"", Content);
        }

        [Fact]
        public async void ReturnTheStringErrorWhenGivenANegative()
        {
            var (StatusCode, Content) = await _triangleClassifier.TriangleType(3, 3, -3);
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"Error\"", Content);
        }

        [Fact]
        public async void ReturnInvalidRequestGivenAFraction()
        {
            var (StatusCode, Content) = await _triangleClassifier.TriangleType(1, 1, 0.333);
            Assert.Equal("400", StatusCode);
            Assert.Equal("{\"message\":\"The request is invalid.\"}", Content);
        }
        
        [Fact]
        public async void WorkWhenParamsAreMax()
        {
            var (StatusCode, Content) = await _triangleClassifier.TriangleType(2147483647, 2147483647, 2147483647);
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"Equilateral\"", Content);
        }

        [Fact]
        public async void ReturnError400WhenParamsAreOver()
        {
            var (StatusCode, Content) = await _triangleClassifier.TriangleType(2147483648, 2147483648, 2147483648);
            Assert.Equal("400", StatusCode);
            Assert.Equal("{\"message\":\"The request is invalid.\"}", Content);
        }

    }
}
