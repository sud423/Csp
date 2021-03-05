using Csp.Models;
using System;

namespace Csp.Core.Models
{
    public class Content : AuditableEntity
    {
        private bool isDeleted;


        public string Name { get; set; }

        public string MetaTitle { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public bool IsPublished { get; set; }

        public DateTimeOffset? PublishedAt { get; set; }

        public bool IsDeleted
        {
            get
            {
                return isDeleted;
            }

            set
            {
                isDeleted = value;
                if (value)
                {
                    IsPublished = false;
                }
            }
        }
    }
}
