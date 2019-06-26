using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Common.FunctionalTests;

namespace WordReverser.FunctionalTests
{
    public interface IWordReverser_HttpClient {
        Task<(string StatusCode, string Content)> ReverseWords(string sentence);
    }

    public class WordReverser_HttpClient : Endpoint_HttpClient, IWordReverser_HttpClient
    {

        public WordReverser_HttpClient(HttpClient client, string hostAddress) :
            base(client, hostAddress)
        {
        }

        public async Task<(string StatusCode, string Content)> ReverseWords(string sentence)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["sentence"] = sentence;
            string queryString = query.ToString();
    
            var ub = new UriBuilder(_hostAddress);
            ub.Path = "/api/reversewords";
            ub.Query = queryString;

            return await base.GetAsync(ub);
        }
    }
}

