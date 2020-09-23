using System;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;

namespace application.helpers
{
    public class HttpClient : System.Net.Http.HttpClient
    {
        private string _queryParams = "";
        private string _resourcePath;
        public HttpClient() : base()
        {
        }

        public async Task<T> GetAsync<T>()
        {
            var request = await base.GetStringAsync(_resourcePath + GetQueryParameters());
            return  JsonConvert.DeserializeObject<T>(request);
        }


        public HttpClient WithUrl(string resourcePath)
        {
            _resourcePath = resourcePath;
            return this;
        }

        public HttpClient AddQueryParameter(string key, object value)
        {
            if (value != null)
            {
                _queryParams += $"&{key}={value.ToString()}";
            }

            return this;
        }

        private string GetQueryParameters() => string.IsNullOrEmpty(_queryParams)
            ? string.Empty 
            : "?" + _queryParams.Substring(1);
    }
}