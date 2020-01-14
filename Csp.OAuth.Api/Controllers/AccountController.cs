using Csp.OAuth.Api.Application;
using Csp.OAuth.Api.Application.Services;
using Csp.OAuth.Api.Infrastructure;
using Csp.OAuth.Api.Models;
using Csp.OAuth.Api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Csp.OAuth.Api.Controllers
{
    [Route("api/v1/account")]
    public class AccountController : ControllerBase
    {
        private readonly OAuthDbContext _ctx;
        private readonly IWxService _wxService;
        ILogger<AccountController> _logger;

        public AccountController(OAuthDbContext ctx, IWxService wxService, ILogger<AccountController> logger)
        {
            _ctx = ctx;
            _wxService = wxService;
            _logger = logger;
        }

        [HttpGet,Route("write")]
        public IActionResult WriteToLog()
        {
            _logger.LogDebug($"oh this is oauth! : {DateTime.UtcNow}");

            return Ok();
        }

        /// <summary>
        /// 根据用户名和密码查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("signin"), HttpPost]
        public async Task<IActionResult> SignInByPassword([FromBody]LoginModel model)
        {
            var user = await _ctx.Users.SingleOrDefaultAsync(a => a.UserName == model.UserName);
            if (user == null || !PasswordHasher.Verify(model.Password, user?.Password))
                return BadRequest(OptResult.Failed("用户名和密码不正确"));

            //记录登录信息

            return Ok(user);
        }

        /// <summary>
        /// 第三方登录
        /// </summary>
        /// <param name="code">第三方登录授权码</param>
        /// <param name="tenantId">租户编号</param>
        /// <returns></returns>
        public async Task<IActionResult> SignInByProvide(string code,int tenantId)
        {
            var login =await _wxService.GetLogin(code);
            if (login == null)
                return BadRequest(OptResult.Failed("授权码无效"));

            var user= await _ctx.Users.Include(a => a.UserLogin).SingleOrDefaultAsync(a => a.UserLogin.OpenId == login.OpenId);

            if(user == null)
            {
                login.TenantId = tenantId;

                user = new User
                {
                    UserLogin = login,
                    Status = 1,
                    CreatedAt = DateTime.Now,
                    TenantId = tenantId
                };

                await _ctx.Users.AddAsync(user);

                await _ctx.SaveChangesAsync();
            }
            else
            {
                //记录登录信息
            }
            return Ok(user);
        }



        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("create"), HttpPost]
        public async Task<IActionResult> Create([FromBody]User user)
        {
            if (!string.IsNullOrEmpty(user.Password))
                user.Password = PasswordHasher.Hash(user.Password);

            _ctx.Users.Add(user);

            await _ctx.SaveChangesAsync();

            return Ok(OptResult.Success());
        }
    }
}
