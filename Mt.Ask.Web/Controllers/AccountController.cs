﻿using Csp.Jwt;
using Csp.Web.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Mt.Ask.Web.Models;
using Mt.Ask.Web.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public AccountController(IAuthService authService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _authService = authService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<IActionResult> Index(string returnUrl = null)
        {
            // If the user is already authenticated we do not need to display the login page, so we redirect to the landing page. 
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }
            string redirectTo = $"{Request.GetDomain()}{Url.Action("RedirectTo", "Account", new { returnUrl })}";
#if !DEBUG
            if (Request.IsMobile())
                return Redirect(await _authService.GetAuthUrl(redirectTo));
#endif
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public async Task<IActionResult> RedirectTo(string code, string returnUrl)
        {
            //ViewBag.Code = code;
            //ViewBag.ReturnUrl = returnUrl;

            var user = await _authService.GetUser(code);

            if (string.IsNullOrEmpty(user.Cell))
            {
                ViewBag.ReturnUrl = returnUrl;
                TempData["User"] = user;
                return View(user);
            }

            return await Sigin(user, returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BindCell(User user,string returnUrl)
        {
            if (!ModelState.IsValid)
                return View("RedirectTo", user);

            var oldUser = TempData["User"] as User;

            oldUser.Cell = user.Cell;
            var result=await _authService.BindCell(user.Cell, oldUser.Id);
            if(!result.Succeed)
            {
                ModelState.AddModelError("Cell", result.Msg);
                return View("RedirectTo", user);
            }


            return await Sigin(oldUser, returnUrl);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            ViewBag.returnUrl = returnUrl;

            
            var user = await _authService.SignByPwd(model);
            if (user == null)
            {
                ModelState.AddModelError("Error", "用户名或密码不正确");
                return View("Index", model);
            }

            return await Sigin(user, returnUrl);
        }

        private async Task<IActionResult> Sigin(User user,string returnUrl)
        {
            var accessTokenResult = _jwtTokenGenerator.GenerateAccessTokenWithClaimsPrincipal(user.UserLogin.UserName, AddMyClaims(user));
            await HttpContext.SignInAsync(accessTokenResult.ClaimsPrincipal, accessTokenResult.AuthProperties);
            return RedirectToLocal(returnUrl);
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Forum");
        }


        private IEnumerable<Claim> AddMyClaims(User user)
        {
            var myClaims = new List<Claim>
            {
                //new Claim(ClaimTypes.Name,user.UserInfo?.Name??""),
                //new Claim(ClaimTypes.Role,dto.Role.ToString()),
                //new Claim(ClaimTypes.Email,user.UserInfo?.Email??""),
                new Claim(ClaimTypes.MobilePhone,user.Cell),
                new Claim(ClaimTypes.NameIdentifier,user.UserLogin?.UserName??(user.ExternalLogin?.NickName??"")),
                new Claim(ClaimTypes.GroupSid,$"{user.TenantId}"),
                new Claim(ClaimTypes.Sid,$"{user.Id}"),
                new Claim("avatar",user.ExternalLogin?.HeadImg??""),
                new Claim("OpenId",user.ExternalLogin?.OpenId??""),
                new Claim("WebSiteId",$"{user.ExternalLogin?.WebSiteId??user.UserLogin?.WebSiteId??0}"),
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
