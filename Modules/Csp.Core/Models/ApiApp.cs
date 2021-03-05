using System.ComponentModel.DataAnnotations;

namespace Csp.Core.Models
{
    public class ApiApp
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "客户端ID不能为空")]
        [StringLength(20,ErrorMessage ="客户端ID最大为20个字符")]
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}
