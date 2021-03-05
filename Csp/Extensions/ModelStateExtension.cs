using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Csp.Web.Extensions
{
    public static class ModelStateExtension
    {
        /// <summary>
        /// 将验证错误的dictionary 第一条信息转换为OptResult
        /// </summary>
        /// <param name="pairs">验证错误dictionary</param>
        /// <returns></returns>
        public static Result ToOptResult(this ModelStateDictionary pairs)
        {
            if (pairs == null)
                return Result.Ok();

            var key = pairs.Keys.FirstOrDefault();

            return Result.Fail(pairs[key].Errors.FirstOrDefault().ErrorMessage);

        }
    }
}
