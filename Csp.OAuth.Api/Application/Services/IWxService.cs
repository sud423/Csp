using Csp.OAuth.Api.Models;
using System.Threading.Tasks;

namespace Csp.OAuth.Api.Application.Services
{
    public interface IWxService
    {
        Task<User> GetLogin(string code,int tenantId,int webSiteId);
    }
}
