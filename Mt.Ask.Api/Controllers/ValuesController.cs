using Csp;
using Csp.EF.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mt.Ask.Api.Infrastructure;
using Mt.Ask.Api.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Mt.Ask.Api.Controllers
{
    [Route("api/v1")]
    public class ValuesController : ControllerBase
    {

        private readonly AskDbContext _askDbContext;

        public ValuesController(AskDbContext askDbContext)
        {
            _askDbContext = askDbContext;
        }

        /// <summary>
        /// 分页获取课程列表
        /// </summary>
        /// <param name="tenantId">所属租户编号</param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet, Route("announces/{tenantId:int}")]
        public async Task<IActionResult> GetAnnounces(int tenantId)
        {
            var result = await _askDbContext.Announces
                .Where(a => a.TenantId == tenantId && a.Status == 1)
                .OrderBy(a => a.Sort).ToListAsync();

            return Ok(result);
        }

        /// <summary>
        /// 根据主键获取通知信息
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns></returns>
        [HttpGet, Route("announces/find/{id:int}")]
        public async Task<IActionResult> GetAnnounceById(int id)
        {
            if (id == 0)
                return BadRequest(OptResult.Failed("id不能小于或为0"));

            var result = await _askDbContext.Announces.SingleOrDefaultAsync(a => a.Id == id);

            return Ok(result);
        }

        /// <summary>
        /// 分页获取课程列表
        /// </summary>
        /// <param name="tenantId">所属租户编号</param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet, Route("courses/{tenantId:int}")]
        public async Task<IActionResult> GetCourses(int tenantId, string academy,string classify)
        {

            var predicate = PredicateExtension.True<Course>();

            predicate.And(a => a.TenantId == tenantId && a.Status == 1);

            if (!string.IsNullOrEmpty(academy))
            {
                predicate.And(a => a.Academy == academy);
            }

            if (!string.IsNullOrEmpty(classify))
            {
                predicate.And(a => a.Classify == classify);
            }


            var result = await _askDbContext.Courses
                .Where(predicate)
                .OrderBy(a => a.Sort).ToListAsync();

            return Ok(result);
        }

        /// <summary>
        /// 根据主键获取课程信息
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns></returns>
        [HttpGet, Route("find/{id:int}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            if (id == 0)
                return BadRequest(OptResult.Failed("id不能小于或为0"));

            var result = await _askDbContext.Courses.SingleOrDefaultAsync(a => a.Id == id);

            return Ok(result);
        }
    }
}
