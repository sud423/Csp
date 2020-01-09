using System;

namespace Csp.EF
{
    public class Entity
    {
        /// <summary>
         /// 创建时间
         /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 最新更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
