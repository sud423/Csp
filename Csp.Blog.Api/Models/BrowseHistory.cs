using Csp.EF;
using System;

namespace Csp.Blog.Api.Models
{
    public class BrowseHistory :Entity
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public int UserId { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }

        public string Device { get; set; }

        public string Os { get; set; }

        public string Source { get; set; }

        public int SourceId { get; set; }

    }
}
