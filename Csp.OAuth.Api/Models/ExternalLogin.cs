using Csp.EF;

namespace Csp.OAuth.Api.Models
{
    public class ExternalLogin : Entity
    {
        public int Id { get; set; }

        public string Provide { get; set; }

        public string OpenId { get; set; }

        public string NickName { get; set; }

        public string HeadImg { get; set; }

        public virtual User User { get; set; }
    }
}
