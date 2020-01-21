using System.ComponentModel.DataAnnotations;

namespace Csp.OAuth.Api.ViewModel
{
    public class UserModel
    {
        public int TenantId { get; set; }

        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "用户名最少4个字符最大16个字符")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "密码最少4个字符最大16个字符")]
        public string Password { get; set; }

        [Required(ErrorMessage = "手机号不能为空")]
        [StringLength(11, MinimumLength = 4, ErrorMessage = "手机号长度为11个字符")]
        [RegularExpression(@"^0?1[3|4|5|7|8|6|9][0-9]\d{8}$", ErrorMessage = "手机号格式不正确")]
        public string Cell { get; set; }

        [StringLength(100, ErrorMessage = "邮箱最大长度为100个字符")]
        //[RegularExpression("",ErrorMessage = "邮箱格式不正确")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }
    }
}
