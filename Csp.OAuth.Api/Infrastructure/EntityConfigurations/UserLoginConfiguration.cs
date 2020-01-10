using Csp.OAuth.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csp.OAuth.Api.Infrastructure.EntityConfigurations
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable("user_login");

            builder.HasKey(a => new { a.UserId, a.OpenId });

            builder.Property(a => a.TenantId).HasColumnName("tenant_id");
            builder.Property(a => a.UserId).HasColumnName("user_id");
            builder.Property(a => a.OpenId).HasColumnName("open_id");
            builder.Property(a => a.NickName).HasColumnName("nick_name");
            builder.Property(a => a.HeadImg).HasColumnName("head_img");
            builder.Property(a => a.CreatedAt).HasColumnName("add_time");

            builder.Ignore(a => a.UpdatedAt);
        }
    }
}
