using Csp.Web.Mvc.Paging;
using Mt.Fruit.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public interface IArticleService
    {

        Task Create(Article article);

        Task Delete(int id);
        
        Task<PagedResult<Article>> GetArticles(int categoryId,int page, int size);

    }
}
