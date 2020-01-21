namespace Csp.Blog.Api.Models
{
    public class ReplyLike
    {
        public int ReplyId { get; set; }

        public int UserId { get; set; }

        public virtual Reply Reply { get; set; }
    }
}
