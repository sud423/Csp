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
        private readonly User _user;

        public ForumController(IArticleService articleService,IIdentityParser<User> parser)
        {
            _user = parser.Parse();
            _articleService = articleService;
        }

        [Route("create/{cid:int}")]
        public ActionResult Create(int cid)
        {
            var model = new Article() { CategoryId = cid };
            return View(model);
        }

        public async Task<ActionResult> Save(Article article)
        {
            if (!ModelState.IsValid)
                return View(nameof(Create), article);

            article.SetId(_user.TenantId,_user.Id,3,article.Id);

            //forum.Id = id;
            var response=await _articleService.Create(article);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("interestgroup", "home", new { id = article.CategoryId });

            var result = await response.GetResult();

            ModelState.AddModelError("Title", result.Msg);

            return View(nameof(Create), article);
        }


    }
}
