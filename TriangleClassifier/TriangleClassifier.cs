using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace TriangleClassifier
{

    public enum TriType
    {
        Equilateral,
        Isosceles,
        Scalene
    }

    public interface ITriangleClassifier
    {
        TriType TriangleType(int a, int b, int c);
    }

    public class TriangleClassifier : ITriangleClassifier
    {
        private static int MAX = 2^32 - 1;
        private static IDictionary<int, TriType> _dictionary = new Dictionary<int, TriType>()
        {
            {1, TriType.Equilateral},
            {2, TriType.Isosceles},
            {3, TriType.Scalene}
        };

        public TriType TriangleType(int a, int b, int c)
        {
            int[] values = {a, b, c}; 
            Array.Sort(values);
            IList<Int32> sides = new List<Int32>(values); 

            long sumOfShortSides = (long) sides[0] + (long) sides[1];
            if (sumOfShortSides <= sides[2])
            {
                throw new ArgumentException("Invalid dimensions for a triangle");
            }

            _dictionary.TryGetValue(sides.Distinct().Count(), out TriType tritype); 
            return tritype;
        }
    }
}

