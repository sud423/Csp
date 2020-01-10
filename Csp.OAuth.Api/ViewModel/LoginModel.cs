using System.ComponentModel.DataAnnotations;

namespace Csp.OAuth.Api.ViewModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(20, ErrorMessage = "用户名最大不能超过20个字符")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(20, ErrorMessage = "密码最大不能超过20个字符")]
        public string Password { get; set; }
    }
}
