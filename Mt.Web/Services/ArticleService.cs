using Microsoft.Extensions.Options;
using Mt.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Web.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IOptions<AppSettings> _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public ArticleService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.OcelotUrl}/blog/api/v1";
        }

        public async Task<Article> GetArticle(int id, string ip, string browser, string device, string os)
        {
            var browse = new BrowseHistory(id,ip,browser,os,device);

            string uri = API.Article.GetArticle(_remoteServiceBaseUrl);

            var forumContent = new StringContent(JsonConvert.SerializeObject(browse), System.Text.Encoding.UTF8, "application/json");
            var responseString = await _httpClient.PostAsync(uri, forumContent);
            var response = JsonConvert.DeserializeObject<Article>(await responseString.Content.ReadAsStringAsync());

            return response;
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            string uri = API.Article.GetArticles(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            var response = JsonConvert.DeserializeObject<IEnumerable<Article>>(responseString);

            return response;
        }
    }
}
