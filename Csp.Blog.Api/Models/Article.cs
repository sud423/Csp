using Csp.EF;

namespace Csp.Blog.Api.Models
{
    public class Article :Entity
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public string Keywrod { get; set; }

        public string Cover { get; set; }

        public string Lead { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public string QuoteUrl { get; set; }

        public byte Status { get; set; }

        public int Clicks { get; set; }

        public int Replys { get; set; }

        public bool IsTop { get; set; }

        public bool IsRed { get; set; }

        public bool IsHot { get; set; }

        public bool IsSlide { get; set; }

        public int Sort { get; set; }

        public string LastReplyUser { get; set; }

        public int UserId { get; set; }

        public virtual Category Category { get; set; }
    }
}
