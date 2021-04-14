using Csp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Csp.EF.Extensions
{
    public static class DbContextExtension
    {
        /// <summary>
        /// 自动初始化或更新基于Entity类的任何类的当前日期时间
        /// </summary>
        public static void AddAuditInfo(this IDbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries().Where(e =>
                e.Entity is AuditableEntity && (e.State is EntityState.Added || e.State is EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State is EntityState.Added)
                {
                    ((AuditableEntity)entry.Entity).CreatedAt = DateTime.Now;
                    ((AuditableEntity)entry.Entity).CreatedBy = dbContext.CurrentUserService.UserId;
                }

                    ((AuditableEntity)entry.Entity).UpdatedAt = DateTime.Now;
                ((AuditableEntity)entry.Entity).UpdatedBy = dbContext.CurrentUserService.UserId;
            }
        }



        public static void ValidateEntities(this DbContext dbContext)
        {
            var modifiedEntries = dbContext.ChangeTracker.Entries()
                    .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in modifiedEntries)
            {
                ICollection<ValidationResult> validationResults = null;
                bool valid = Validator.TryValidateObject(entity, new ValidationContext(entity, null, null), validationResults, true);

                //if(valid)
                //    throw new ValidationException(entity.Entity.GetType(), validationResults);
            }
        }

    }
}
