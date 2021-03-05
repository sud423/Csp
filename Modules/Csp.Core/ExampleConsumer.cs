using Csp.Core.Data;
using Csp.Core.Models;
using System;
using System.Threading.Tasks;

namespace Csp.Core
{
    internal sealed class ExampleConsumer
    {
        private readonly ICoreDbContext _dbContext;

        public ExampleConsumer(ICoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Run()
        {
            ApiApp app = new ApiApp()
            {
                ClientSecret = Guid.NewGuid().ToString()
            };

            _dbContext.ApiApps.Add(app);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
