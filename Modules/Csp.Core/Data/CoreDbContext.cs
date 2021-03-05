using Csp.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Csp.Core.Data
{
    internal class CoreDbContext : DbContext, ICoreDbContext
    {
        public DbSet<ApiApp> ApiApps { get; set; }

        public CoreDbContext()
        {

        }

        public CoreDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}
