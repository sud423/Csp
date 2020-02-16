using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mt.Fruit.Web.Models;
using Mt.Fruit.Web.Services;

namespace Mt.Fruit.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public HomeController(ILogger<HomeController> logger, 
            ICategoryService categoryService,
            IArticleService articleService)
        {
            _logger = logger;

            _articleService = articleService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetHotCategories("article");

            return View(result);
        }

        [Route("/group")]
        public async Task<IActionResult> Group()
        {
            var result = await _categoryService.GetCategories("article");

            return View(result);
        }

        [Route("/interestgroup/{id:int}")]
        async public Task<IActionResult> InterestGroup(int id)
        {
            ViewBag.CategoryId = id;
            var result = await _categoryService.GetCategory(id);

            return View(result);
        }

        [Route("/interesting")]
        public IActionResult Interesting()
        {
            return View();
        }

        [Route("/vedio")]
        public IActionResult Vedio()
        {
            return View();
        }

        [Route("/list/{categoryId:int}")]
        public async Task<IActionResult> List(int categoryId,int page = 1)
        {
            var result = await _articleService.GetArticles(categoryId, page, 20);

            return Ok(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
