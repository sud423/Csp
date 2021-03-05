using Csp.Data;
using Csp.Extensions;
using System;
using System.Reflection;

namespace Csp.Proxy
{
    public class DbContextProxy<TDecorated> : DispatchProxy where TDecorated: IDbContext
    {
        private TDecorated _decorated;

        private ICurrentUserService _currentUserService;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {

            //可添加实体验证,待实现
            _decorated.ValidateEntities();

            //添加审计信息
            _decorated.AddAuditInfo(_currentUserService);
            
            var result = targetMethod.Invoke(_decorated, args);
            //领域事件发布，待实现
            return result;
        }

        public static TDecorated Create(TDecorated decorated, ICurrentUserService currentUserService)
        {
            object proxy = Create<TDecorated, DbContextProxy<TDecorated>>();
            ((DbContextProxy<TDecorated>)proxy).SetParameters(decorated, currentUserService);

            return (TDecorated)proxy;
        }

        private void SetParameters(TDecorated decorated, ICurrentUserService currentUserService)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }
            _decorated = decorated;
            _currentUserService = currentUserService;
        }
    }
}
