using Csp.EF;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Csp.Blog.Api.Models
{
    public class Category : Entity
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        [Required(ErrorMessage = "分类名称不能为空")]
        [StringLength(60,ErrorMessage = "分类名称最大为60个字符")]
        public string Name { get; set; }

        [Required(ErrorMessage = "分类类型不能为空")]
        [StringLength(60, ErrorMessage = "分类类型最大为20个字符")]
        public string Type { get; set; }

        public string Code { get; set; }

        public int Followers { get; set; }

        public int Sort { get; set; } 

        public byte Status { get; set; } 

        public string Icon { get; set; }

        public string Remark { get; set; }

        public int UserId { get; set; }

        public ICollection<Article> Articles { get; set; }

        public ICollection<Reply> Replies { get; set; }

        public ICollection<CategoryLike> CategoryLikes { get; set; }

        public Category()
        {
            Articles = new List<Article>();
            Replies = new List<Reply>();
            CategoryLikes = new List<CategoryLike>();
            Status = 1;
            Sort = 10001;
        }
    }
}
