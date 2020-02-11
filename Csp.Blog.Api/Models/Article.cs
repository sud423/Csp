using Csp.EF;
using System.ComponentModel.DataAnnotations;

namespace Csp.Blog.Api.Models
{
    public class Article :Entity
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public int CategoryId { get; set; }

        [Required(ErrorMessage ="标题不能为空")]
        [StringLength(100,ErrorMessage ="标题最大为100个字符")]
        public string Title { get; set; }

        [StringLength(255, ErrorMessage = "关键字最大为255个字符")]
        public string Keywrod { get; set; }

        public string Cover { get; set; }

        [StringLength(255, ErrorMessage = "导语最大为255个字符")]
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

        [StringLength(50, ErrorMessage = "最后回复用户最大为50个字符")]
        public string LastReplyUser { get; set; }

        public int UserId { get; set; }

        public virtual Category Category { get; set; }
    }
}
