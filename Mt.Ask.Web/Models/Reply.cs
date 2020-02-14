using System;
using System.Collections.Generic;

namespace Mt.Ask.Web.Models
{
    public class Reply
    {
        public int Id { get; set; }

        public int ForumId { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }

        public int Likes { get; set; }

        public DateTime CreatedAt { get; set; }

        public ExternalLogin ExternalLogin { get; set; }

        public IEnumerable<ReplyLike> ReplyLikes { get; set; }
    }

    public class ReplyLike
    {
        public int UserId { get; set; }
    }
}
