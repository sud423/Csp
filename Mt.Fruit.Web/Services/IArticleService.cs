using Csp.Web.Mvc.Paging;
using Mt.Fruit.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public interface IArticleService
    {

        Task<HttpResponseMessage> Create(Article article);

        Task<HttpResponseMessage> Delete(int id);
        
        Task<PagedResult<Article>> GetArticles(int categoryId,int page, int size);

    }
}
