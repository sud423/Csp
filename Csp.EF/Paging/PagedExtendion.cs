using System;
using System.Linq;

namespace Csp.EF.Paging
{
    public static class PagedExtendion
    {
        public static PagedResult<T> ToPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = query.Count()
            };


            var skip = (page - 1) * pageSize;
            result.Data = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
