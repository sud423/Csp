using Microsoft.Extensions.Options;
using Mt.Fruit.Web.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IOptions<AppSettings> _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public AuthService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.OcelotUrl}/u/api/v1/account";
        }

        public async Task Create(RegModel model)
        {
            string uri = API.Auth.Create(_remoteServiceBaseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();
        }


        public async Task<User> SignByPwd(LoginModel model)
        {
            string uri = API.Auth.UserLogin(_remoteServiceBaseUrl);
            
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<User>(responseString);
        }
    }
}
