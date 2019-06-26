using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Common.FunctionalTests
{

    public abstract class Endpoint_HttpClient
    {

        protected readonly HttpClient _client;
        protected readonly string _hostAddress;

        public Endpoint_HttpClient(HttpClient client, string hostAddress)
        {
            _client = client;
            _hostAddress = hostAddress;
        }


        protected async Task<(string StatusCode, string Content)> GetAsync(UriBuilder ub)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("requesting: " + ub.ToString());
            HttpResponseMessage response =  await _client.GetAsync(ub.Uri);
            Console.WriteLine("response: " + response.ToString());
            var statusCode = response.StatusCode.ToString("D");
            var content = await response.Content.ReadAsStringAsync();

            return (statusCode, content);
        }

    }
}

