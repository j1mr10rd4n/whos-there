using System;
using Xunit;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace WordReverser.FunctionalTests
{
    public class WordReverser_ReverseWordsEndpointShould
    {

        private readonly IWordReverser_HttpClient _wordReverser;

        public WordReverser_ReverseWordsEndpointShould()
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
            _wordReverser = new WordReverser_HttpClient(client, host);
        }

        [Fact]
        public async void ReverseASingleWord()
        {
            var (StatusCode, Content) = await _wordReverser.ReverseWords("foo");
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"oof\"", Content);
        }

        [Fact]
        public async void RetainWhitespaceAroundASingleWord()
        {
            var (StatusCode, Content) = await _wordReverser.ReverseWords("   foo     ");
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"   oof     \"", Content);
        }

        [Fact]
        public async void RetainOrderOfMultipleWords()
        {
            var (StatusCode, Content) = await _wordReverser.ReverseWords("foobar bazqux");
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"raboof xuqzab\"", Content);
        }

        [Fact]
        public async void RetainOrderAndWhitespaceAroundMultipleWords()
        {
            var (StatusCode, Content) = await _wordReverser.ReverseWords("  foobar    bazqux    ");
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"  raboof    xuqzab    \"", Content);
        }

        [Fact]
        public async void TreatPunctuationAsAWord()
        {
            var (StatusCode, Content) = await _wordReverser.ReverseWords("!@#$%^&*'");
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"'*&^%$#@!\"", Content);
        }

        [Fact]
        public async void TreatTabsAsWhitespace() 
        {
            var (StatusCode, Content) = await _wordReverser.ReverseWords("foobar\u0009bazqux");
            Assert.Equal("200", StatusCode);
            Assert.Equal(@"""raboof\txuqzab""", Content);
        }

        [Fact]
        public async void ReturnEmptyStringGivenWhitespace()
        {
            var (StatusCode, Content) = await _wordReverser.ReverseWords("    ");
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"\"", Content);
        }

        [Fact]
        public async void ReturnEmptyStringGivenTabbedWhitespace()
        {
            var (StatusCode, Content) = await _wordReverser.ReverseWords("\t\t\t");
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"\"", Content);
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
        public async void ReverseReallyLongString()
        {
            var (Forward, Reversed) = RandomChars(2039);
            var (StatusCode, Content) = await _wordReverser.ReverseWords(new String(Forward));
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"" + new String(Reversed) + "\"", Content);
        }

        [Fact]
        public async void ErrorOnReallyReallyLongString()
        {
            var (Forward, Reversed) = RandomChars(2040);
            var (StatusCode, Content) = await _wordReverser.ReverseWords(new String(Forward));
            Assert.Equal("404", StatusCode);
            Assert.Equal("The resource you are looking for has been removed, had its name changed, or is temporarily unavailable.", Content);
        }

        [Fact]
        public async void ReverseExtendedUnicodeCharacters()
        {
            var (StatusCode, Content) = await _wordReverser.ReverseWords("\u9FFF\u4E00");
            Assert.Equal("200", StatusCode);
            Assert.Equal("\"\u4E00\u9FFF\"", Content);
        }
    }
}
