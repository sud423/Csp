using Csp.EF;

namespace Csp.OAuth.Api.Models
{
    public class User : Entity
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Cell { get; set; }

        public string Email { get; set; }

        public byte Status { get; set; }

        public string Avatar { get; set; }

        /// <summary>
        /// 最后审核原因
        /// </summary>
        public string Audit { get; set; }


        public virtual UserLogin UserLogin { get; set; }

    }
}
