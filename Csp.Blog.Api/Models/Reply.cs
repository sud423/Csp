using Csp.EF;
using System.Collections.Generic;

namespace Csp.Blog.Api.Models
{
    public class Reply :Entity
    {
        public int Id { get; set; }

        public string Source { get; set; }

        public int SourceId { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public int UserId { get; set; }

        public virtual Article Article { get; set; }

        public virtual ICollection<ReplyLike> ReplyLikes { get; set; }
    }
}
