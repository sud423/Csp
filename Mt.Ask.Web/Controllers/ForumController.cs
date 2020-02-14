using Csp.Jwt;
using Csp.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mt.Ask.Web.Models;
using Mt.Ask.Web.Services;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly User _user;


        public ForumController(IArticleService articleService,IIdentityParser<User> parser)
        {
            _articleService = articleService;
            _user = parser.Parse();
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var result =await _articleService.GetArticleByPage(1, 0, page, 20);

            return View(result);
        }
        
        public async Task<IActionResult> List(int page = 1)
        {
            var result =await _articleService.GetArticleByPage(1, _user.Id, page, 20);

            return View(result);
        }

        public async Task<IActionResult> Post(int? id)
        {
            ViewBag.Id = id.GetValueOrDefault();
            if (id.HasValue && id > 0)
            {
                var vm = await _articleService.GetArticle(id.Value);
                return View(vm);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, Article article)
        {
            if (!ModelState.IsValid)
                return View(nameof(Post), article);

            article.Id = id;
            article.CategoryId = 1;

            article.UserId = _user.Id;
            article.TenantId = _user.TenantId;

            article.WebSiteId = _user.UserLogin.WebSiteId;

            await _articleService.Create(article);

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var article = await _articleService.GetArticle(id, HttpContext.RemoteIp(), Request.BrowserNameByUserAgent(), Request.DeviceByUserAgent(), Request.OsByUserAgent(),_user.Id); ;
            //if(vm!=null && vm.Id > 0)
            //{
            //    var request = Domain.Statistics.CreateForumStastic(Request.IpByUserAgent(), Request.BrowserNameByUserAgent(), Request.OsNameByUserAgent(), id);
            //}

#if !DEBUG
            var config =await Util.GetConfig($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.Path.Value}{HttpContext.Request.QueryString.Value}");

            ViewBag.WxConfig = config;
#endif
            return View(article);
        }

        public async Task<IActionResult> GetReplies(int id, int page=1)
        {
            return Ok(await _articleService.GetReplies(id, page, 20));
        }
    }
}
