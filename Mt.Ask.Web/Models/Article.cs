using System;
using System.ComponentModel.DataAnnotations;

namespace Mt.Ask.Web.Models
{
    public class Article
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [StringLength(100,ErrorMessage = "标题最大为100个字符")]
        [Required(ErrorMessage ="标题不能为空")]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Clicks { get; set; }

        public int Replys { get; set; }

        public byte Status { get; set; }

        public int UserId { get; set; }

        public int TenantId { get; set; }

        public int WebSiteId { get; set; }

        public virtual User AppUser { get; set; }
    }
}
