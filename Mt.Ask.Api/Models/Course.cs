﻿using Csp.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mt.Ask.Api.Models
{
    public class Course : Entity
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public string Academy { get; set; }

        public string Classify { get; set; }

        public string Name { get; set; }

        public string BaseInfo { get; set; }

        public string BackSummary { get; set; }

        public string TeaSummary { get; set; }

        public string Summary { get; set; }

        public string OpenDate { get; set; }

        public string Address { get; set; }

        public decimal Fee { get; set; }

        public int Sort { get; set; }

        public byte Status { get; set; }

        public int UserId { get; set; }


    }
}
