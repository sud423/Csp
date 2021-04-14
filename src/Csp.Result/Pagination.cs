using System;
using System.Collections.Generic;

namespace Csp.Result
{
    public class Pagination<T>
    {
        /// <summary>
        /// 数据列表项
        /// </summary>
        public IEnumerable<T> Items { get; }

        /// <summary>
        /// 索引页码，从1开始
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
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage => PageIndex > 1;

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage => PageIndex < TotalPages;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items">数据列表项</param>
        /// <param name="count">总记录数</param>
        /// <param name="pageIndex">索引页</param>
        /// <param name="pageSize">每页记录数</param>
        public Pagination(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }

    }
}
