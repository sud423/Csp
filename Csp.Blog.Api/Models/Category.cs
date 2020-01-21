using Csp.EF;
using System.Collections.Generic;

namespace Csp.Blog.Api.Models
{
    public class Category : Entity
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Code { get; set; }

        public int Followers { get; set; }

        public int Sort { get; set; } = 100001;

        public string Icon { get; set; }

        public string Remark { get; set; }

        public ICollection<Article> Articles { get; set; }

        public ICollection<Reply> Replies { get; set; }

        public ICollection<CategoryLike> CategoryLikes { get; set; }
    }
}
