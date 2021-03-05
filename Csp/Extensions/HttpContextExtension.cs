using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace Csp.Extensions
{
    /// <summary>
    /// HttpContext扩展
    /// </summary>
    public static class HttpContextExtension
    {
        #region exception and statuscode 扩展
        /// <summary>
        /// api 异常时响应内容
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        public async static Task ExceptionResponse(this HttpContext context)
        {
            //判断 context-type是否有设置，false则设置为appliction/json
            if (string.IsNullOrWhiteSpace(context.Response.ContentType))
                context.Response.ContentType = "application/json; charset=utf-8";


            var feature = context.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;

            //重设响应内容
            await context.Response.WriteAsync(Result.Fail(error?.Message).ToString());

        }

        /// <summary>
        /// 状态不为200时重置响应内容
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        public async static Task StatusCodeResponse(this HttpContext context)
        {
            //判断 context-type是否有设置，false则设置为appliction/json
            if (string.IsNullOrWhiteSpace(context.Response.ContentType))
                context.Response.ContentType = "application/json; charset=utf-8";

            //判断是否有响应的内容且状态不为200，如果没有内容则设置响应内容
            if ((!context.Response.ContentLength.HasValue || context.Response.ContentLength == 0) && context.Response.StatusCode != 200)
            {
                await context.Response.WriteAsync(Result.Fail(context.Response.StatusCode.StatusCodeToString()).ToString());
            }
        }

        /// <summary>
        /// 请求状态转字符串
        /// </summary>
        /// <param name="code">状态码</param>
        /// <returns></returns>
        internal static string StatusCodeToString(this int code)
        {
            return ((HttpStatusCode)code).ToString();
        }
        #endregion

        /// <summary>
        /// 获取客户端ip地址
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        public static string RemoteIp(this HttpContext context)
        {
            return context.Connection.RemoteIpAddress.ToString();
        }
    }
}
