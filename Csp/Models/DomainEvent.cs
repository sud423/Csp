using System;
using System.Collections.Generic;

namespace Csp.Models
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }

    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// 发生日期
        /// </summary>
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
