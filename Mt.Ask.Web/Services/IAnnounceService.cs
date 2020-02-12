using Mt.Ask.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Services
{
    public interface IAnnounceService
    {
        Task<Article> GetAnnounce(int id);

        Task<IEnumerable<Article>> GetAnnounces(int size);
    }
}
