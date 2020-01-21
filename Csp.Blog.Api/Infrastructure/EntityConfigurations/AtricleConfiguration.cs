using Csp.Blog.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csp.Blog.Api.Infrastructure.EntityConfigurations
{
    public class AtricleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("article");

            builder.HasKey(a => a.Id);
        }
    }
}
