using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Csp.EF
{
    public class Pagination<T>
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<T> Data { get; }

        /// <summary>
        /// 索引页
        /// </summary>
        public int PageIndex { get; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// 是否含有上一页
        /// </summary>
        public bool HasPreviousPage => PageIndex > 1;

        /// <summary>
        /// 是否含有下一页
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;


        public Pagination(List<T> data, int totalCount, int pageIndex, int pageSize)
        {
            Data = data;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }


        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new Pagination<T>(items, count, pageIndex, pageSize);
        }
    }
}
