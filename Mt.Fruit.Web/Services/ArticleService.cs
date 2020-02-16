using Csp.Web.Mvc.Paging;
using Microsoft.Extensions.Options;
using Mt.Fruit.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
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


        public async Task Create(Article article)
        {
            string uri = API.Article.Create(_remoteServiceBaseUrl);

            var forumContent = new StringContent(JsonConvert.SerializeObject(article), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, forumContent);

            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            string uri = API.Article.Delete(_remoteServiceBaseUrl, id);

            var response = await _httpClient.PutAsync(uri, null);

            response.EnsureSuccessStatusCode();
        }

        public async Task<PagedResult<Article>> GetArticles(int categoryId, int page, int size)
        {
            string uri = API.Article.GetArticles(_remoteServiceBaseUrl, categoryId,page, size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonConvert.DeserializeObject<PagedResult<Article>>(responseString);

            return result;
        }
    }
}
