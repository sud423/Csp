using Csp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csp.Core.Data.Configurations
{
    class ApiAppConfiguration : IEntityTypeConfiguration<ApiApp>
    {
        public void Configure(EntityTypeBuilder<ApiApp> builder)
        {
            builder.HasKey(a => a.Id);

            builder.ToTable("apiapp");
        }
    }
}
