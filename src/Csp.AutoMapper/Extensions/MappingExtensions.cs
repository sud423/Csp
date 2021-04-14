using AutoMapper;
using AutoMapper.QueryableExtensions;
using Csp.EF.Extensions;
using Csp.Result;
using System.Linq;
using System.Threading.Tasks;

namespace Csp.AutoMapper.Extensions
{
    public static class MappingExtensions
    {
        public static Task<Pagination<TDestination>> ToPagedAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => IQueryableExtension.ToPagedAsync(queryable,pageNumber, pageSize);

        /// 之前版本将List<TDestination>改成IQueryable<TDestination>
        public static Task<IQueryable<TDestination>> ToMapperAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
            => Task.FromResult(queryable.ProjectTo<TDestination>(configuration));
    }
}
