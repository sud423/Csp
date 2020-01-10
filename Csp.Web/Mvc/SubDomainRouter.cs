using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace Csp.Web.Mvc
{
    public class SubDomainRouter : RouteBase
    {
        private readonly IRouter _target;
        private readonly string _subDomain;

        public SubDomainRouter(IRouter target,
            //string subDomain,//当前路由规则绑定的二级域名
            string template, 
            string name, 
            IInlineConstraintResolver constraintResolver, 
            RouteValueDictionary defaults, 
            IDictionary<string, object> constraints, 
            RouteValueDictionary dataTokens) 
            : base(template, name, constraintResolver, defaults, constraints, dataTokens)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            _subDomain = name;
            _target = target;
        }

        protected override Task OnRouteMatched(RouteContext context)
        {
            context.RouteData.Routers.Add(_target);
            return _target.RouteAsync(context);
        }

        protected override VirtualPathData OnVirtualPathGenerated(VirtualPathContext context)
        {
            return _target.GetVirtualPath(context);
        }

        public override Task RouteAsync(RouteContext context)
        {
            string domain = context.HttpContext.Request.Host.Host;//获取当前请求域名，然后跟_subDomain比较，如果不想等，直接忽略

            if (string.IsNullOrEmpty(domain) || string.Compare(_subDomain, domain) != 0)
            {
                return Task.CompletedTask;
            }

            //如果域名匹配，再去验证访问路径是否匹配

            return base.RouteAsync(context);

        }

    }
}
