using Csp.Blog.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Csp.Blog.Api.Infrastructure.EntityConfigurations
{
    public class ReplyLikeConfiguration : IEntityTypeConfiguration<ReplyLike>
    {
        public void Configure(EntityTypeBuilder<ReplyLike> builder)
        {
            builder.ToTable("replylike");

            builder.HasKey(a =>new { a.ReplyId,a.UserId });
        }
    }
}
