using System.ComponentModel.DataAnnotations;

namespace Mt.Ask.Web.Models
{
    public class User
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        [StringLength(11,ErrorMessage ="手机号最大为11个字符")]
        [RegularExpression(@"^0?1[3|4|5|7|8|6|9][0-9]\d{8}$", ErrorMessage = "手机号码格式不正确")]
        [Required(ErrorMessage ="手机号不能为空")]
        public string Cell { get; set; }

        public byte Status { get; set; }

        public virtual ExternalLogin ExternalLogin { get; set; }

        public virtual UserLogin UserLogin { get; set; }
    }
}
