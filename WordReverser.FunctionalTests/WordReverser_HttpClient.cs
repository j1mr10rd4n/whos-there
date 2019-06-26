using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WordReverser.FunctionalTests
{
    public interface IWordReverser_HttpClient {
        Task<(string StatusCode, string Content)> ReverseWords(string sentence);
    }

    public class WordReverser_HttpClient : IWordReverser_HttpClient
    {

        private readonly HttpClient _client;
        private readonly string _hostAddress;

        public WordReverser_HttpClient(HttpClient client, string hostAddress)
        {
            _client = client;
            _hostAddress = hostAddress;
        }

        public async Task<(string StatusCode, string Content)> ReverseWords(string sentence)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["sentence"] = sentence;
            string queryString = query.ToString();
    
            var ub = new UriBuilder(_hostAddress);
            ub.Path = "/api/reversewords";
            ub.Query = queryString;

            Console.WriteLine("requesting: " + ub.ToString());
            HttpResponseMessage response =  await _client.GetAsync(ub.Uri);
            Console.WriteLine("response: " + response.ToString());

            var statusCode = response.StatusCode.ToString("D");
            var content = await response.Content.ReadAsStringAsync();

            return (statusCode, content);
        }
    }
}

