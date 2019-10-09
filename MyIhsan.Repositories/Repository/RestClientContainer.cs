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

namespace MyIhsan.Repositories.Repository
{
    public class RestClientContainer<T> where T:class,new()
    {
        private readonly RestClient client;
        private readonly string _serverUri;
        public RestClientContainer(string serverUri)
        {
            _serverUri = serverUri;
            client = new RestClient();
        }        
        public Task<T> Get(string uri, string accessToken = null)
        {
            client.CookieContainer = new CookieContainer();
            var taskCompletionSource = new TaskCompletionSource<T>();
            var request = new RestRequest($"{_serverUri}{uri}", Method.GET);
            if (accessToken != null) request.AddParameter("Authorization", "Bearer " + accessToken, ParameterType.HttpHeader);
            client.ExecuteAsync<T>(request, (response) => taskCompletionSource.SetResult(response.Data));
            return taskCompletionSource.Task;
        }
        public Task<T> Post(string uri,object obj,string accessToken = null)
        {
            client.CookieContainer = new CookieContainer();
            var taskCompletionSource = new TaskCompletionSource<T>();
            var request = new RestRequest($"{_serverUri}{uri}", Method.POST).AddJsonBody(obj);
            if (accessToken != null) request.AddParameter("Authorization", "Bearer " + accessToken, ParameterType.HttpHeader);
            client.ExecuteAsync<T>(request, (response) => taskCompletionSource.SetResult(response.Data));
            return taskCompletionSource.Task;
        }
        public Task<T> Put(string uri, object obj, string accessToken = null)
        {
            client.CookieContainer = new CookieContainer();
            var taskCompletionSource = new TaskCompletionSource<T>();
            var request = new RestRequest($"{_serverUri}{uri}", Method.PUT).AddJsonBody(obj);
            if (accessToken != null) request.AddParameter("Authorization", "Bearer " + accessToken, ParameterType.HttpHeader);
            client.ExecuteAsync<T>(request, (response) => taskCompletionSource.SetResult(response.Data));
            return taskCompletionSource.Task;
        }
        public Task<T> Delete(string uri, string accessToken = null)
        {
            client.CookieContainer = new CookieContainer();
            var taskCompletionSource = new TaskCompletionSource<T>();
            var request = new RestRequest($"{_serverUri}{uri}", Method.DELETE);
            if (accessToken != null) request.AddParameter("Authorization", "Bearer " + accessToken, ParameterType.HttpHeader);
            client.ExecuteAsync<T>(request, (response) => taskCompletionSource.SetResult(response.Data));
            return taskCompletionSource.Task;
        }
    }
}
