using Mt.Fruit.Web.Models;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public interface IAuthService
    {

        Task Create(RegModel model);

        Task<User> SignByPwd(LoginModel model);
    }
}
