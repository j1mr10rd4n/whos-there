using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Common.FunctionalTests;

namespace TriangleClassifier.FunctionalTests
{
    public interface ITriangleClassifier_HttpClient {
        Task<(string StatusCode, string Content)> TriangleType(Object a, Object b, Object c);
    }

    public class TriangleClassifier_HttpClient : Endpoint_HttpClient, ITriangleClassifier_HttpClient
    {

        public TriangleClassifier_HttpClient(HttpClient client, string hostAddress) :
            base(client, hostAddress)
        {
        }

        public async Task<(string StatusCode, string Content)> TriangleType(Object a, Object b, Object c)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["a"] = a.ToString();
            query["b"] = b.ToString();
            query["c"] = c.ToString();
            string queryString = query.ToString();
    
            var ub = new UriBuilder(_hostAddress);
            ub.Path = "/api/TriangleType";
            ub.Query = queryString;

            return await base.GetAsync(ub);
        }
    }
}

