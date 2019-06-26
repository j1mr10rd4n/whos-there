using System;
using Xunit;
using TriangleClassifier;

namespace TriangleClassifier.UnitTests
{
    public class TriangleClassifier_TriangleTypeShould
    {
        private readonly TriangleClassifier _triangleClassifier;

        public TriangleClassifier_TriangleTypeShould()
        {
            _triangleClassifier = new TriangleClassifier();
        }

        [Fact]
        public void CorrectlyIdentifyAScaleneTriangle()
        {
            var triangleType = _triangleClassifier.TriangleType(3, 4, 5);
            Assert.Equal(TriType.Scalene, triangleType);
        }

        [Fact]
        public void CorrectlyIdentifyAnIsoscelesTriangle()
        {
            var triangleType = _triangleClassifier.TriangleType(3, 5, 5);
            Assert.Equal(TriType.Isosceles, triangleType);
        }

        [Fact]
        public void ClassifiyAnEquilateralTriangle()
        {
            var triangleType = _triangleClassifier.TriangleType(3, 3, 3);
            Assert.Equal(TriType.Equilateral, triangleType);
        }

        [Fact]
        public void ClassifiyAScaleneTriangle()
        {
            var triangleType = _triangleClassifier.TriangleType(3, 4, 5);
            Assert.Equal(TriType.Scalene, triangleType);
        }

        [Fact]
        public void ClassifiyAnIsoscelesTriangle()
        {
            var triangleType = _triangleClassifier.TriangleType(3, 3, 5);
            Assert.Equal(TriType.Isosceles, triangleType);
        }

        [Fact]
        public void ReturnTheStringErrorWhenGivenAZero()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => _triangleClassifier.TriangleType(3, 3, 0));
            Assert.Equal("Invalid dimensions for a triangle", ex.Message);
        }

        [Fact]
        public void ReturnTheStringErrorWhenGivenAnImpossibleTriangle()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => _triangleClassifier.TriangleType(1, 1, 2));
            Assert.Equal("Invalid dimensions for a triangle", ex.Message);
        }

        [Fact]
        public void ReturnTheStringErrorWhenGivenANegative()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => _triangleClassifier.TriangleType(3, 3, -3));
            Assert.Equal("Invalid dimensions for a triangle", ex.Message);
        }
        
        [Fact]
        public void WorkWhenParamsAreMax()
        {
            var triangleType = _triangleClassifier.TriangleType(2147483647, 2147483647, 2147483647);
            Assert.Equal(TriType.Equilateral, triangleType);
        }

        // [Fact]
        // public void ReturnError400WhenParamsAreOver()
        // {
        //     var triangleType = _triangleClassifier.TriangleType(2147483648, 2147483648, 2147483648);
        //     Assert.Equal("{\"message\":\"The request is invalid.\"}", triangleType);
        // }
        
    }
}
