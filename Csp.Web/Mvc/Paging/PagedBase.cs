using System;

namespace Csp.Web.Mvc.Paging
{
    public abstract class PagedBase
    {
        /// <summary>
        /// 当前页码，从1开始
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get
            {
                var pageCount = (double)TotalCount / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage => CurrentPage - 1 > 0;

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage => CurrentPage < TotalPage;
    }
}
