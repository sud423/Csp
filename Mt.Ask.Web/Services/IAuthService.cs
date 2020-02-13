using Csp;
using Mt.Ask.Web.Models;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Services
{
    public interface IAuthService
    {
        Task<string> GetAuthUrl(string redirectUrl);

        Task<User> GetUser(string code);

        Task<OptResult> BindCell(string cell, int userId);

        Task<User> SignByPwd(LoginModel model);
    }
}
