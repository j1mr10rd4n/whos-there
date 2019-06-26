using System;
using System.Text;
using System.Text.RegularExpressions;

namespace WordReverser
{
    public class WordReverser
    {
        private static int MAX_LENGTH = 2040;

        private string ReverseWord(string word) {
            char[] arr = word.ToCharArray();
            Array.Reverse(arr);
            return new String(arr);
        }

        public string ReverseWords(string inputString)
        {
            if (inputString.Length >= MAX_LENGTH) {
                throw new ArgumentException("Input string must be fewer than " + MAX_LENGTH + " characters");
            }

            StringBuilder sb = new StringBuilder();
            MatchCollection matches = Regex.Matches(inputString, @"(\s+)?((\S+)(\s*)+?)");
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                  GroupCollection groups = match.Groups;
                  sb.Append(groups[1].Value);
                  sb.Append(ReverseWord(groups[3].Value));
                  sb.Append(groups[4].Value);
                }
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}

