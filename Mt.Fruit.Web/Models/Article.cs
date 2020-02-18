using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mt.Fruit.Web.Models
{
    public class Article
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [StringLength(100, ErrorMessage = "标题最大为100个字符")]
        [Required(ErrorMessage = "标题不能为空")]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Clicks { get; set; }

        public int Replys { get; set; }

        public int Likes { get; set; }

        public string LastReplyUser { get; set; }

        [JsonIgnore]
        public byte Status { get; set; } = 1;

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public int TenantId { get; set; }

        [JsonIgnore]
        public int WebSiteId { get; set; }

        public virtual User User { get; set; }

        public void SetId(int tenantId, int userId, int webSiteId, int id)
        {
            Id = id;
            TenantId = tenantId;
            UserId = userId;

            WebSiteId = webSiteId;
        }
    }
}
