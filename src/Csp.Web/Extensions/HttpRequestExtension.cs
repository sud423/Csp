using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace Csp.Web.Extensions
{
    /// <summary>
    /// HttpRequest扩展
    /// </summary>
    public static class HttpRequestExtension
    {
        static readonly Regex b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        static readonly Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        /// <summary>
        /// 判断当前请求是否为ajax
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        //public static bool IsAjax(this HttpRequest req)
        //{

        //    return true;

        //    bool result = false;

        //    var xreq = req.Headers.ContainsKey("x-requested-with");
        //    if (xreq)
        //    {
        //        result = req.Headers["x-requested-with"] == "XMLHttpRequest";
        //    }

        //    return result;
        //}

        /// <summary>
        /// 获取请求头User-Agent
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string UserAgent(this HttpRequest request)
        {
            return request.Headers["User-Agent"];
        }

        /// <summary>
        /// 判断 客户是否为移动端
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsMobile(this HttpRequest request)
        {

            var userAgent = request.UserAgent();

            if ((b.IsMatch(userAgent) || v.IsMatch(userAgent.Substring(0, 4))))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取设备名字
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static string DeviceByUserAgent(this HttpRequest request)
        {
            string userAgent = request.UserAgent();
            var m = b.Match(userAgent);
            string osVersion = m.Value;
            return osVersion;
        }

        /// <summary>
        /// 获取浏览器
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string BrowserNameByUserAgent(this HttpRequest request)
        {
            string userAgent = request.UserAgent();
            string fullBrowserName = string.Empty;
            // IE
            string regexStr = @"msie (?<ver>[\d.]+)";
            Regex r = new Regex(regexStr, RegexOptions.IgnoreCase);
            Match m = r.Match(userAgent);
            string ver;
            string browserName;
            if (m.Success)
            {
                browserName = "IE";
                ver = m.Groups[1].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            // Firefox
            regexStr = @"firefox\/([\d.]+)";
            r = new Regex(regexStr, RegexOptions.IgnoreCase);
            m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "Firefox";
                ver = m.Groups[1].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            // Chrome
            regexStr = @"chrome\/([\d.]+)";
            r = new Regex(regexStr, RegexOptions.IgnoreCase);
            m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "Chrome";
                ver = m.Groups[1].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            // Opera
            regexStr = @"opera.([\d.]+)";
            r = new Regex(regexStr, RegexOptions.IgnoreCase);
            m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "Opera";
                ver = m.Groups[1].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            // Safari
            regexStr = @"version\/([\d.]+).*safari";
            r = new Regex(regexStr, RegexOptions.IgnoreCase);
            m = r.Match(userAgent);
            if (m.Success)
            {
                browserName = "Safari";
                ver = m.Groups[1].Value;
                fullBrowserName = string.Format("{0} {1}", browserName, ver);
                return fullBrowserName;
            }
            return fullBrowserName;
        }

        /// <summary>
        /// 根据 User Agent 获取操作系统名称
        /// </summary>
        public static string OsByUserAgent(this HttpRequest request)
        {
            string userAgent = request.UserAgent();
            string osVersion = "Unknown";
            if (string.IsNullOrEmpty(userAgent))
            {
                return osVersion;
            }

            if (userAgent.Contains("Windows NT 6.1"))
            {
                osVersion = "Windows 7";
            }
            else if (userAgent.Contains("Windows NT 6.0"))
            {
                osVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("Windows NT 5.2"))
            {
                osVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("Windows NT 5.1"))
            {
                osVersion = "Windows XP";
            }
            else if (userAgent.Contains("Windows NT 5"))
            {
                osVersion = "Windows 2000";
            }
            else if (userAgent.Contains("Windows NT 4"))
            {
                osVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Windows Me"))
            {
                osVersion = "Windows Me";
            }
            else if (userAgent.Contains("Windows 98"))
            {
                osVersion = "Windows 98";
            }
            else if (userAgent.Contains("Windows 95"))
            {
                osVersion = "Windows 95";
            }
            else if (userAgent.Contains("Windows"))
            {
                osVersion = "Windows";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                osVersion = "SunOS";
            }
            return osVersion;
        }

        /// <summary>
        /// 获取当前请求的域名
        /// 返回结果如：http://www.example.com
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetDomain(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host.Value}";
        }

        /// <summary>
        /// 获取当前请求地址
        /// 返回结果如：http://www.example.com/home
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetCurrentPath(this HttpRequest request)
        {
            //判断是否有地址
            if (request.Path.HasValue)
                return $"{request.GetDomain()}{request.Path.Value}";

            return request.GetDomain();
        }

        /// <summary>
        /// 获取当前请求完整地址包含querystring
        /// 返回结果如：http://www.example.com?page=1&size=10
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetCurrentUrl(this HttpRequest request)
        {
            //判断是否有地址栏参数
            if (request.QueryString.HasValue)
                return $"{request.GetCurrentPath()}{request.QueryString.Value}";

            return request.GetCurrentPath();
        }

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
