using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Common.FunctionalTests;

namespace Fibonacci.FunctionalTests
{
    public interface IFibonacci_HttpClient {
        Task<(string StatusCode, string Content)> NthFibonacciNum(Object nc);
    }

    public class Fibonacci_HttpClient : Endpoint_HttpClient, IFibonacci_HttpClient
    {

        public Fibonacci_HttpClient(HttpClient client, string hostAddress) :
            base(client, hostAddress)
        {
        }

        public async Task<(string StatusCode, string Content)> NthFibonacciNum(Object n)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["n"] = n.ToString();
            string queryString = query.ToString();
    
            var ub = new UriBuilder(_hostAddress);
            ub.Path = "/api/Fibonacci";
            ub.Query = queryString;

            return await base.GetAsync(ub);
        }
    }
}

