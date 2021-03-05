using Csp.Data;
using Csp.Exceptions;
using Csp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Csp.Extensions
{
    public static class DbContextExtension
    {
        /// <summary>
        /// 自动初始化或更新基于Entity类的任何类的当前日期时间
        /// </summary>
        public static void AddAuditInfo(this IDbContext dbContext,ICurrentUserService currentUserService)
        {
            var entries = dbContext.ChangeTracker.Entries().Where(e =>
                e.Entity is AuditableEntity && (e.State is EntityState.Added || e.State is EntityState.Modified));

            foreach (var entry in entries)
            {
                var auditable = ((AuditableEntity)entry.Entity);
                if (entry.State is EntityState.Added)
                {
                    auditable.CreatedAt = DateTime.Now;

                    auditable.CreatedBy = currentUserService.UserId;

                    auditable.UpdatedBy = 0;
                }
                else
                {
                    auditable.UpdatedBy = currentUserService.UserId;
                }
                auditable.UpdatedAt = DateTime.Now;
            }
        }


        public static void ValidateEntities(this IDbContext dbContext)
        {
            var modifiedEntries = dbContext.ChangeTracker.Entries()
                    .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in modifiedEntries)
            {
                ICollection<ValidationResult> validationResults=null;
                bool valid = Validator.TryValidateObject(entity, new ValidationContext(entity, null, null), validationResults, true);

                //if(valid)
                //    throw new ValidationException(entity.Entity.GetType(), validationResults);
            }
        }
    }
}
