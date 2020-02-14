using System.Collections.Generic;

namespace Csp.Web.Mvc.Paging
{
    public class PagedResult<T> : PagedBase where T:class
    {
        public IEnumerable<T> Data { get; set; }

        public PagedResult() { }

        public PagedResult(IEnumerable<T> data)
        {
            Data = data;
        }
    }
}
