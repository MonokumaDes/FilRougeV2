using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace FilRouge.MVC.API
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetAsync<T>(this HttpClient client, string url, string token) where T : class
        {
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                client.DefaultRequestHeaders.Authorization = null;
            }

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }

        public static async Task<T> PostAsFormAsync<T>(this HttpClient client, string url, IEnumerable<KeyValuePair<string, string>> formData) where T : class
        {
            var response = client.PostAsync(url, new FormUrlEncodedContent(formData)).Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }

        public static async Task<T> PostAsJsonAsync<T>(this HttpClient client, string url, object body, string token) where T : class
        {
            var json = JsonConvert.SerializeObject(body);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }

        public static async Task<bool> PatchAsJsonAsync<T>(this HttpClient client, string url, object body, string token)
        {
            var json = JsonConvert.SerializeObject(body);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpRequestMessage msg = new HttpRequestMessage(new HttpMethod("PATCH"), url);
            msg.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(msg);

            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteAsJsonAsync<T>(this HttpClient client, string url, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }
    }
}