using Csp.Result;
using System.Linq;
using System.Threading.Tasks;

namespace Csp.EF.Extensions
{
    public static class IQueryableExtension
    {

        public static Pagination<T> ToPaged<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var count = query.Count();
            var items = query.Skip((page - 1) * pageSize).Take(pageSize);
            var result = new Pagination<T>(items, count, page, pageSize);
            return result;
        }

        public static Task<Pagination<T>> ToPagedAsync<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var result = query.ToPaged(page,pageSize);

            return Task.FromResult(result);
        }
    }
}
