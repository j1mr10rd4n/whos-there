using System;
using Xunit;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace TriangleClassifier.FunctionalTests
{
    public class TriangleClassifier_TriangleTypeEndpointShould
    {

        private readonly ITriangleClassifier_HttpClient _triangleClassifier;

        public TriangleClassifier_TriangleTypeEndpointShould()
        {
            string host = Environment.GetEnvironmentVariable("WHOS_THERE_API_HOST");
            if (String.IsNullOrEmpty(host)) {
                throw new ArgumentException("Did you forget to set the $WHOS_THERE_API_HOST environment variable?");
            }
            HttpClient client;
            var isSecureLocalhost = Regex.Match(host, @"^https://localhost");
            if (isSecureLocalhost.Success) {
                var certificateIgnoringHttpClientHandler = new HttpClientHandler();
                certificateIgnoringHttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                client = new HttpClient(certificateIgnoringHttpClientHandler);
            } else {
                client = new HttpClient();
            }
            _triangleClassifier = new TriangleClassifier_HttpClient(client, host);
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
