using System;
using System.Threading.Tasks;
using Template10.Services.Web;
using Windows.Web.Http;

namespace Tenplex.Models
{
    public class ApiService
    {
        private HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient();
        }

        public void AddHeader(string key, string value)
        {
            if (_client.DefaultRequestHeaders.ContainsKey(key))
                _client.DefaultRequestHeaders.Remove(key);

            _client.DefaultRequestHeaders.Add(key, value);
        }

        public Task DeleteAsync(Uri path)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> GetAsync(Uri path)
        {
            var response = await _client.GetAsync(path);
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(Uri path, IHttpContent payload)
        {
            var response = await _client.PostAsync(path, payload);
            return response;
        }

        public Task PutAsync(Uri path, IHttpContent payload)
        {
            throw new NotImplementedException();
        }
    }
}
