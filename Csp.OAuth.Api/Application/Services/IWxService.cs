using Csp.OAuth.Api.Models;
using System.Threading.Tasks;

namespace Csp.OAuth.Api.Application.Services
{
    public interface IWxService
    {
        Task<ExternalLogin> GetLogin(string code);
    }
}
