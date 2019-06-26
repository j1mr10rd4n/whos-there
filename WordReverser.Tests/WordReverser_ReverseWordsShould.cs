using System;
using Xunit;
using WordReverser;

namespace WordReverser.UnitTests
{
    public class WordReverser_ReverseWordsShould
    {
        private readonly WordReverser _wordReverser;

        public WordReverser_ReverseWordsShould()
        {
            _wordReverser = new WordReverser();
        }

        [Fact]
        public void ReverseASingleWord()
        {
            var reversed = _wordReverser.ReverseWords("foo");
            Assert.Equal("oof", reversed);
        }

        [Fact]
        public void RetainWhitespaceAroundASingleWord()
        {
            var reversed = _wordReverser.ReverseWords("   foo     ");
            Assert.Equal("   oof     ", reversed);
        }

        [Fact]
        public void RetainOrderOfMultipleWords()
        {
            var reversed = _wordReverser.ReverseWords("foobar bazqux");
            Assert.Equal("raboof xuqzab", reversed);
        }

        [Fact]
        public void RetainOrderAndWhitespaceAroundMultipleWords()
        {
            var reversed = _wordReverser.ReverseWords("  foobar    bazqux    ");
            Assert.Equal("  raboof    xuqzab    ", reversed);
        }

        [Fact]
        public void TreatPunctuationAsAWord()
        {
            var reversed = _wordReverser.ReverseWords("!@#$%^&*'");
            Assert.Equal("'*&^%$#@!", reversed);
        }

        [Fact]
        public void TreatTabsAsWhitespace() 
        {
            var reversed = _wordReverser.ReverseWords("foobar\u0009bazqux");
            Assert.Equal("raboof\u0009xuqzab", reversed);
        }

        [Fact]
        public void ReturnEmptyStringGivenWhitespace()
        {
            var reversed = _wordReverser.ReverseWords("    ");
            Assert.Equal("", reversed);
        }

        [Fact]
        public void ReturnEmptyStringGivenTabbedWhitespace()
        {
            var reversed = _wordReverser.ReverseWords("\t\t\t");
            Assert.Equal("", reversed);
        }

        private (char[] Forward, char[] Reversed) RandomChars(int length)
        {
            Random random = new Random();
            var chars = new char[length];
            var rchars = new char[length];
            for (int i = 0; i < length; i++)
            {
                int x = random.Next(0,26);
                char c = (char)('a' + x);
                chars[i] = c; 
                rchars[length - i - 1] = c; 
            }
            return (chars, rchars);
        }

        [Fact]
        public void ReverseReallyLongString()
        {
            var (Forward, Reversed) = RandomChars(2039);
            var reversed = _wordReverser.ReverseWords(new String(Forward));
            Assert.Equal("" + new String(Reversed) + "", reversed);
        }

        [Fact]
        public void ErrorOnReallyReallyLongString()
        {
            var (Forward, Reversed) = RandomChars(2040);
            Exception ex = Assert.Throws<ArgumentException>(() => _wordReverser.ReverseWords(new String(Forward)));
            Assert.Equal("Input string must be fewer than 2040 characters", ex.Message);
        }

        [Fact]
        public void ReverseExtendedUnicodeCharacters()
        {
            var reversed = _wordReverser.ReverseWords("\u9FFF\u4E00");
            Assert.Equal("\u4E00\u9FFF", reversed);
        }

        
    }
}
