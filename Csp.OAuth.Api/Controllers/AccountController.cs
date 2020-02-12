using Csp.Jwt;
using Csp.OAuth.Api.Application;
using Csp.OAuth.Api.Application.Services;
using Csp.OAuth.Api.Infrastructure;
using Csp.OAuth.Api.Models;
using Csp.OAuth.Api.ViewModel;
using Csp.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Csp.OAuth.Api.Controllers
{
    [Route("api/v1/account")]
    public class AccountController : ControllerBase
    {
        private readonly OAuthDbContext _ctx;
        private readonly IWxService _wxService;
        readonly ILogger<AccountController> _logger;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AccountController(OAuthDbContext ctx, IWxService wxService, ILogger<AccountController> logger, IJwtTokenGenerator jwtTokenGenerator)
        {
            _ctx = ctx;
            _wxService = wxService;
            _logger = logger;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [Route("sigin")]
        [HttpPost]
        public async Task<IActionResult> SigIn([FromBody]LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.First());

            var user = await _ctx.Users.Include(a => a.UserLogin).SingleOrDefaultAsync(a => a.UserLogin.UserName == model.UserName);

            if (user == null || !PasswordHasher.Verify(model.Password, user.UserLogin?.Password))
                return BadRequest(OptResult.Failed("用户名和密码不正确"));

            var accessTokenResult = _jwtTokenGenerator.GenerateAccessTokenWithClaimsPrincipal(model.UserName, AddMyClaims(user));

            return Ok(accessTokenResult.AccessToken);
        }

        /// <summary>
        /// 根据用户名和密码查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("signin"), HttpPost]
        public async Task<IActionResult> SignInByPassword([FromBody]LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.First());

            var user = await _ctx.Users.Include(a=>a.UserLogin).SingleOrDefaultAsync(a => a.UserLogin.UserName == model.UserName);
            if (user == null || !PasswordHasher.Verify(model.Password, user.UserLogin?.Password))
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

            var user= await _ctx.Users.Include(a => a.ExternalLogin).SingleOrDefaultAsync(a => a.ExternalLogin.OpenId == login.OpenId);

            if(user == null)
            {
                user = new User
                {
                    ExternalLogin = login,
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
                _logger.LogInformation($"{user.ExternalLogin?.OpenId}-{DateTimeOffset.UtcNow.LocalDateTime} 登录成功");
            }
            return Ok(user);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("create"), HttpPost]
        public async Task<IActionResult> Create([FromBody]UserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.First());

            var userLogin = new UserLogin
            {
                UserName = model.UserName,
                Password = PasswordHasher.Hash(model.Password)                
            };

            var userInfo = new UserInfo
            {
                Cell = model.Cell,
                Email = model.Email,
                Sex = Sex.Unknown                
            };

            var user = new User
            {
                UserInfo = userInfo,
                UserLogin = userLogin,
                Status = 1,
                TenantId = model.TenantId                
            };

            _ctx.Users.Add(user);

            await _ctx.SaveChangesAsync();

            return Ok(OptResult.Success());
        }


        private IEnumerable<Claim> AddMyClaims(User user)
        {
            var myClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserInfo?.Name??""),
                //new Claim(ClaimTypes.Role,dto.Role.ToString()),
                new Claim(ClaimTypes.Email,user.UserInfo?.Email??""),
                new Claim(ClaimTypes.MobilePhone,user.UserInfo?.Cell??""),
                new Claim(ClaimTypes.NameIdentifier,user.UserLogin?.UserName??(user.ExternalLogin?.NickName??"")),
                new Claim(ClaimTypes.GroupSid,$"{user.TenantId}"),
                new Claim(ClaimTypes.Sid,$"{user.Id}"),
                new Claim("avatar",user.UserInfo?.Avatar??(user.ExternalLogin?.HeadImg??"")),
                new Claim("open_id",user.ExternalLogin?.OpenId??""),
                new Claim("aud", "OAuth"),
                new Claim("aud", "Blog"),
                new Claim("aud", "Upload"),
                new Claim("aud", "AskApi"),
                new Claim("aud", "AskWeb"),
                new Claim("aud", "MtWeb")
            };

            return myClaims;
        }
    }
}
