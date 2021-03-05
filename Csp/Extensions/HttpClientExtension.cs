using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Csp.Extensions
{
    public static class HttpClientExtension
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent httpContent)
        {
            var readerString = await httpContent.ReadAsStringAsync();

            return readerString.FromJson<T>();
        }

        public static async Task<T> PostJsonAsync<T>(this HttpClient client, string url, object obj=null)
        {
            StringContent content = null;
            if (obj != null)
                 content = new StringContent(obj.ToJsonString(), Encoding.UTF8, "application/json");

            return await client.PostAsync(url, content).Result.Content.ReadAsJsonAsync<T>();
        }

        public static async Task<T> PutJsonAsync<T>(this HttpClient client, string url, object obj=null)
        {
            StringContent content = null;
            if (obj != null)
                content = new StringContent(obj.ToJsonString(), Encoding.UTF8, "application/json");

            return await client.PutAsync(url, content).Result.Content.ReadAsJsonAsync<T>();
        }


    }
}
