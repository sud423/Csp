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
    public class MyController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IResourceService _resourceService;
        private readonly ICategoryService _categoryService;
        private readonly User _user;

        public MyController(IArticleService articleService, 
            IResourceService resourceService,
            IIdentityParser<User> parser,
            ICategoryService categoryService)
        {
            _user = parser.Parse();
            _articleService = articleService;
            _resourceService = resourceService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Categories(int page)
        {
            var result = await _categoryService.GetCategoryByPage("both", _user.Id, page, 20);

            return Ok(result);
        }


        public async Task<IActionResult> Category(int id)
        {
            var result = await _categoryService.GetCategory(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Category(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            var response = await _categoryService.Create(category);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            else
                return View(category);
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
