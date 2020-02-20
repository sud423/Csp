using Csp.Jwt;
using Csp.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mt.Fruit.Web.Models;
using Mt.Fruit.Web.Services;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IResourceService _resourceService;
        private readonly User _user;

        public ForumController(IArticleService articleService, IResourceService resourceService,IIdentityParser<User> parser)
        {
            _user = parser.Parse();
            _articleService = articleService;
            _resourceService = resourceService;
        }

        /// <summary>
        /// 发布水果文章
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        [Route("create/{cid:int}")]
        public IActionResult Create(int cid)
        {
            if (cid <= 0)
                return NotFound();

            var model = new Article() { CategoryId = cid };
            return View(model);
        }

        /// <summary>
        /// 发布趣闻杂谈
        /// </summary>
        /// <returns></returns>
        public IActionResult Post()
        {
            var model = new Article { CategoryId = 43 };
            return View(model);
        }

        public IActionResult Resource(int cid=44)
        {
            var resouce = new Resource { CategoryId = cid };
            return View(resouce);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Article article)
        {
            if (!ModelState.IsValid)
                if (article.CategoryId == 43)
                    return View(nameof(Post), article);
                else
                    return View(nameof(Create), article);

            article.SetId(_user.TenantId,_user.Id,_user.NickName,3,article.Id);

            //forum.Id = id;
            var response=await _articleService.Create(article);

            if (response.IsSuccessStatusCode)
                if (article.CategoryId == 43)
                    return RedirectToAction("interesting", "home");
                else
                    return RedirectToAction("interestgroup", "home", new { id = article.CategoryId });

            var result = await response.GetResult();

            ModelState.AddModelError("Title", result.Msg);

            if (article.CategoryId == 43)
                return View(nameof(Post), article);

            return View(nameof(Create), article);
        }
        
        [HttpPost]
        public async Task<IActionResult> Commit(Resource resource)
        {
            if (!ModelState.IsValid)
                return View(nameof(Resource), resource);

            resource.SetId(_user.TenantId, _user.Id, _user.NickName, 3, resource.Id);

            //forum.Id = id;
            var response = await _resourceService.Create(resource);

            if (response.IsSuccessStatusCode)
                if (resource.CategoryId != 44)
                    return RedirectToAction("interestgroup", "home", new { id = resource.CategoryId });
                else
                    return RedirectToAction("video", "home");

            var result = await response.GetResult();

            ModelState.AddModelError("Title", result.Msg);

            return View(nameof(Resource), resource);
        }

    }
}
