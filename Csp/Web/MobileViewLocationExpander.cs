using Csp.Extensions;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;
using System.Linq;

namespace Csp.Web
{
    /// <summary>
    /// mvc视图文件路径重写
    /// 主要作用是pc和移动共用一个controller
    /// 控制呈现不同的视图
    /// </summary>
    public class MobileViewLocationExpander : IViewLocationExpander
    {
        private readonly string _name;

        /// <summary>
        /// 移动端默认为Mobile
        /// PC端为Views
        /// </summary>
        public MobileViewLocationExpander()
        {
            _name = "Mobile";
        }

        /// <summary>
        /// 设置移动端视图的根文件夹名称
        /// </summary>
        /// <param name="name"></param>
        public MobileViewLocationExpander(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 替换原有Views
        /// </summary>
        /// <param name="context"></param>
        /// <param name="viewLocations"></param>
        /// <returns></returns>
        public virtual IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            viewLocations = viewLocations.Select(s => s.Replace("/Views/", $"/{context.Values["Device"]}/"));

            return viewLocations;
        }

        /// <summary>
        /// 判断是否移动端来决定是否变更Views
        /// </summary>
        /// <param name="context"></param>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            string viewName = context.ActionContext.HttpContext.Request.IsMobile() ? _name : "Views";

            context.Values["Device"] = viewName;

        }
    }
}
