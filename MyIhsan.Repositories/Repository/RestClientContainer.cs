using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyIhsan.Repositories.Repository
{
    public class RestClientContainer
    {
        private readonly RestClient client;
        private readonly string _serverUri;
        public RestClientContainer(string serverUri)
        {
            _serverUri = serverUri;
            client = new RestClient();
        }
        public async Task<T> SendRequest<T>(string uri, Method method, object obj = null)
        {
            client.CookieContainer = new CookieContainer();
            var request = new RestRequest($"{_serverUri}{uri}", method);
            if (method == Method.POST || method == Method.PUT)
            {
                request.AddJsonBody(obj);
            }
            var accessToken = HttpContext.Current.Request.Cookies["token"]?.Value;
            if (accessToken != null) request.AddParameter("Authorization", "Bearer " + accessToken, ParameterType.HttpHeader);
            var response = client.ExecuteTaskAsync<T>(request).GetAwaiter().GetResult();
            return response.Data;
        }
    }
}
