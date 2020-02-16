﻿using Microsoft.Extensions.Options;
using Mt.Fruit.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IOptions<AppSettings> _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public CategoryService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.OcelotUrl}/blog/api/v1";
        }

        public async Task<IEnumerable<Category>> GetCategories(string type)
        {
            string uri = API.Category.GetCategories(_remoteServiceBaseUrl, type);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonConvert.DeserializeObject<IEnumerable<Category>>(responseString);

            return result;
        }

        public async Task<Category> GetCategory(int id)
        {
            string uri = API.Category.GetCategory(_remoteServiceBaseUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonConvert.DeserializeObject<Category>(responseString);

            return result;
        }

        public async Task<IEnumerable<Category>> GetHotCategories(string type)
        {
            string uri = API.Category.GetHotCategories(_remoteServiceBaseUrl, type);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = JsonConvert.DeserializeObject<IEnumerable<Category>>(responseString);

            return result;
        }
    }
}
