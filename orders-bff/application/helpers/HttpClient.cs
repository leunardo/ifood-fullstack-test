using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;

namespace application.helpers
{
    public class HttpClient : System.Net.Http.HttpClient
    {
        protected string _queryParams = "";
        protected string _resourcePath;
        public HttpClient() : base() {}

        public HttpClientExecutable WithUrl(string resourcePath)
        {
            _resourcePath = resourcePath;
            return this.ToExecutable();
        }

        public HttpClient AddQueryParameter(string key, object value)
        {
            if (value != null)
            {
                _queryParams += $"&{key}={value.ToString()}";
            }

            return this;
        }

        protected HttpClientExecutable ToExecutable() => new HttpClientExecutable(_queryParams, _resourcePath);

        public class HttpClientExecutable : HttpClient
        {
            public HttpClientExecutable(string queryParams, string resourcePath) : base()
            {
                base._queryParams = queryParams;
                base._resourcePath = resourcePath;
                this.DefaultRequestHeaders.Add("Accept", "application/json");
            }

            public async Task<T> GetAsync<T>()
            {
                var request = await base.GetStringAsync(_resourcePath + GetQueryParameters());
                return  JsonConvert.DeserializeObject<T>(request);
            }

            public async Task<T> PostAsync<T, U>(U body)
            {
                var payload = JsonConvert.SerializeObject(body);
                var request = await base.PostAsync(
                    _resourcePath + GetQueryParameters(),
                    new StringContent(payload, Encoding.UTF8, "application/json")
                );

                var response = await request.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(response);
            }

            private string GetQueryParameters() => string.IsNullOrEmpty(_queryParams)
                ? string.Empty 
                : "?" + _queryParams.Substring(1);
        }
    }
}