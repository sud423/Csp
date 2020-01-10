using Csp.EF;
using Newtonsoft.Json;

namespace Csp.OAuth.Api.Models
{
    public class UserLogin : Entity
    {
        public int TenantId { get; set; }

        public int UserId { get; set; }

        public byte Provide { get; set; }

        public string OpenId { get; set; }

        public string NickName { get; set; }

        [JsonProperty("HeadImgUrl")]
        public string HeadImg { get; set; }

        public virtual User User { get; set; }

    }
}
