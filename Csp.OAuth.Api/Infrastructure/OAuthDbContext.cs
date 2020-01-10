using Csp.EF.Extensions;
using Csp.OAuth.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Csp.OAuth.Api.Infrastructure
{
    public class OAuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }




        public OAuthDbContext(DbContextOptions<OAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations<OAuthDbContext>();
        }
        public override int SaveChanges()
        {
            this.AddAuditInfo();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            this.AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
