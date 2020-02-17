using Csp.Web.Mvc.Paging;
using Microsoft.Extensions.Options;
using Mt.Ask.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Services
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

            var response = await _httpClient.DeleteAsync(uri);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteReply(int replyId)
        {
            string uri = API.Article.DeleteReply(_remoteServiceBaseUrl, replyId);

            var response = await _httpClient.DeleteAsync(uri);

            response.EnsureSuccessStatusCode();
        }

        public async Task<Article> GetArticle(int id, string ip, string browser, string device, string os, int userId = 0)
        {
            var browse = new BrowseHistory(id, ip, browser, os, device,userId);

            string uri = API.Article.GetArticle(_remoteServiceBaseUrl);

            var forumContent = new StringContent(JsonConvert.SerializeObject(browse), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, forumContent);
            var result = JsonConvert.DeserializeObject<Article>(await response.Content.ReadAsStringAsync());

            return result;
        }

        public async Task<Article> GetArticle(int id)
        {
            string uri = API.Article.GetArticle(_remoteServiceBaseUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonConvert.DeserializeObject<Article>(responseString);

            return result;
        }

        public async Task<PagedResult<Article>> GetArticleByPage(int categoryId, int userId, int page, int size)
        {
            string uri = API.Article.GetArticleByPage(_remoteServiceBaseUrl, categoryId, userId, page, size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonConvert.DeserializeObject<PagedResult<Article>>(responseString);

            return result;
        }

        public async Task<IEnumerable<Article>> GetArticles(int categoryId)
        {
            string uri = API.Article.GetArticles(_remoteServiceBaseUrl, categoryId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonConvert.DeserializeObject<IEnumerable<Article>>(responseString);

            return result;
        }

        public async Task<IEnumerable<Article>> GetArticles(int categoryId,int size)
        {
            string uri = API.Article.GetArticles(_remoteServiceBaseUrl,categoryId,size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonConvert.DeserializeObject<IEnumerable<Article>>(responseString);

            return result;
        }

        public async Task<PagedResult<Reply>> GetReplies(int id, int page, int size)
        {
            string uri = API.Article.GetReplyByPage(_remoteServiceBaseUrl, id, page, size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonConvert.DeserializeObject<PagedResult<Reply>>(responseString);

            return result;
        }

        public async Task<WxConfig> GetWxConfig(string url)
        {
            string uri = $"{_settings.Value.OcelotUrl}/api/v1/wx/getconfig?url={url}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonConvert.DeserializeObject<WxConfig>(responseString);

            return result;
        }
    }
}
