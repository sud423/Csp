using Csp.EF.Extensions;
using Csp.Upload.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Csp.Upload.Api.Infrastructure
{
    public class OssDbContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }




        public OssDbContext(DbContextOptions<OssDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations<OssDbContext>();
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
