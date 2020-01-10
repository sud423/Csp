using Csp.OAuth.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csp.OAuth.Api.Infrastructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.TenantId).HasColumnName("tenant_id");
            builder.Property(a => a.UserName).HasColumnName("user_name");
            builder.Property(a => a.Audit).HasColumnName("audit_reason");
            builder.Property(a => a.CreatedAt).HasColumnName("add_time");

            builder.Ignore(a => a.UpdatedAt);

            builder.HasOne(a => a.UserLogin).WithOne(a => a.User).HasForeignKey<User>(a => a.Id);
        }
    }
}
