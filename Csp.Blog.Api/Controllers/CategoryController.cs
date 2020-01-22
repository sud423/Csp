using Csp.Blog.Api.Infrastructure;
using Csp.Blog.Api.Models;
using Csp.EF.Paging;
using Csp.Jwt;
using Csp.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Csp.Blog.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly BlogDbContext _blogDbContext;
        private readonly AppUser _appUser;


        public CategoryController(BlogDbContext blogDbContext,IIdentityParser<AppUser> parser)
        {
            _blogDbContext = blogDbContext;
            _appUser = parser.Parse();
        }

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(int page,int size)
        {
            var result = _blogDbContext.Categories
                .Where(a=>a.TenantId==_appUser.TenantId && a.Status==1)
                .OrderBy(a => a.Sort).ToPaged(page, size);

            return Ok(result);
        }

        /// <summary>
        /// 根据主键获取分类信息
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns></returns>
        [HttpGet,Route("find/{id:int}")]
        public IActionResult FindById(int id)
        {
            if (id == 0)
                return BadRequest(OptResult.Failed("id不能小于或为0"));

            var result = _blogDbContext.Categories.SingleOrDefault(a => a.Id == id);

            return Ok(result);
        }

        /// <summary>
        /// 创建或更新分类
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public IActionResult Create([FromBody]Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.First());

            if (category.Id > 0)
            {
                _blogDbContext.Categories.Update(category);
            }
            else
            {
                category.UserId = _appUser.Id;
                category.TenantId = _appUser.TenantId;
                _blogDbContext.Categories.Add(category);
            }

            _blogDbContext.SaveChanges();

            return Ok(OptResult.Success());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">根据主键删除</param>
        /// <returns></returns>
        [HttpPut,Route("delete/{id:int}")]
        public IActionResult Deprecated(int id)
        {
            var category= _blogDbContext.Categories.Include(a=>a.Articles).SingleOrDefault(a => a.Id == id);
            
            if (category == null || category.Id < 0)
                return BadRequest(OptResult.Failed("删除的数据不存在"));

            category.Status = 0;
            category.Articles?.ToList()?.ForEach(a => {
                a.Status = 0;
            });

            _blogDbContext.Categories.Update(category);
            _blogDbContext.SaveChanges();
            return Ok(OptResult.Success());

        }
        /// <summary>
        /// 关注或取关
        /// </summary>
        /// <param name="id">根据主键关注或取关</param>
        /// <returns></returns>
        [HttpPut, Route("attention/{id}")]
        public IActionResult Attention(int id)
        {
            var category = _blogDbContext.Categories.Include(a => a.CategoryLikes)
                .SingleOrDefault(a => a.Id == id);

            if (category == null || category.Id == 0)
                return BadRequest(OptResult.Failed("关注的分类不存在"));

            //存在删除
            if(category.CategoryLikes.Any(b => b.UserId == _appUser.Id))
            {
                category.Followers -= 1;
                category.CategoryLikes.Remove(category.CategoryLikes.First(a => a.UserId == _appUser.Id));
            }
            else
            {
                category.Followers += 1;
                CategoryLike cl = new CategoryLike(id, _appUser.Id);
                category.CategoryLikes.Add(cl);
            }
            _blogDbContext.Categories.Update(category);

            _blogDbContext.SaveChanges();
            return Ok(OptResult.Success());
        }
    }
}
