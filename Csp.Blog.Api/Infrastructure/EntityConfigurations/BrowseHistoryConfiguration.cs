﻿using Csp.Blog.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Csp.Blog.Api.Infrastructure.EntityConfigurations
{
    public class BrowseHistoryConfiguration : IEntityTypeConfiguration<BrowseHistory>
    {
        public void Configure(EntityTypeBuilder<BrowseHistory> builder)
        {
            builder.ToTable("browsehistory");

            builder.HasKey(a => a.Id);

            builder.Ignore(a => a.UpdatedAt);
        }
    }
}
