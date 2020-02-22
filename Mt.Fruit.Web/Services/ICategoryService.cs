﻿using Mt.Fruit.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public interface ICategoryService
    {
        Task<HttpResponseMessage> Create(Category category);

        Task<IEnumerable<Category>> GetCategories(string type);

        Task<Category> GetCategory(int id);

        Task<IEnumerable<Category>> GetHotCategories(string type);

    }
}
