using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading.Tasks;

namespace Csp.Extensions
{
    public static class MappingExtension
    {
        public static Task<Pagination<TDestination>> ToPagedAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => Pagination<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        /// 之前版本将List<TDestination>改成IQueryable<TDestination>
        public static Task<IQueryable<TDestination>> ToMapperAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
            => Task.FromResult(queryable.ProjectTo<TDestination>(configuration));
    }
}
