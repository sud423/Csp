using System.Net.Http;
using System.Threading.Tasks;

namespace Csp.Web.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<OptResult> GetResult(this HttpResponseMessage httpResponse)
        {
            return await httpResponse.GetResult<OptResult>();
        }

        public static async Task<T> GetResult<T>(this HttpResponseMessage httpResponse) where T:class
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = responseString.FromJson<T>();

            return result;
        }
    }
}
