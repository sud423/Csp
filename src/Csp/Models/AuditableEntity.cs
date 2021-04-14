using System;

namespace Csp.Models
{
    /// <summary>
    /// 可追踪实体
    /// </summary>
    public class AuditableEntity
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 最后更新人
        /// </summary>
        public int UpdatedBy { get; set; }
    }
}
