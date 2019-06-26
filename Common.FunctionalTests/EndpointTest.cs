using System;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Common.FunctionalTests
{
    public abstract class EndpointTest
    {

        protected readonly HttpClient _client;
        protected readonly string _host;

        public EndpointTest()
        {
            _host = Environment.GetEnvironmentVariable("WHOS_THERE_API_HOST");
            if (String.IsNullOrEmpty(_host)) {
                throw new ArgumentException("Did you forget to set the $WHOS_THERE_API_HOST environment variable?");
            }
            HttpClient client;
            var isSecureLocalhost = Regex.Match(_host, @"^https://localhost");
            if (isSecureLocalhost.Success) {
                var certificateIgnoringHttpClientHandler = new HttpClientHandler();
                certificateIgnoringHttpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                _client = new HttpClient(certificateIgnoringHttpClientHandler);
            } else {
                _client = new HttpClient();
            }
        }
    }
}
