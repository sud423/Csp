using Csp.Core.Models;
using Csp.Data;
using Microsoft.EntityFrameworkCore;

namespace Csp.Core.Data
{
    internal interface ICoreDbContext : IDbContext
    {
        public DbSet<ApiApp> ApiApps { get; set; }
    }
}
