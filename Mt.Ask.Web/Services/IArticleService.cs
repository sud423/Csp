using Mt.Ask.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Services
{
    public interface IArticleService
    {
        Task<Article> GetArticle(int id, string ip, string browser, string device, string os);

        Task<IEnumerable<Article>> GetArticles(int categoryId);

        Task<IEnumerable<Article>> GetArticles(int categoryId, int size);
    }
}
