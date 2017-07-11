using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models
{
    public class DiscountsViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public Nullable<decimal> Persent { get; set; }
        public Nullable<decimal> FixPrice { get; set; }

        public Nullable<decimal> Millem { get; set; }
        public Nullable<decimal> MinOrderGram { get; set; }
        public Nullable<decimal> MinOrderPrice { get; set; }
        public System.DateTime PeriodFrom { get; set; }
        public System.DateTime PeriodTo { get; set; }
        public int CompanyId { get; set; }

        public virtual Companies Companies { get; set; }
    }

    public class DiscountsListItem
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public Nullable<decimal> Persent { get; set; }
        public Nullable<decimal> FixPrice { get; set; }
        public Nullable<decimal> MinOrderGram { get; set; }
        public Nullable<decimal> MinOrderPrice { get; set; }
        public System.DateTime PeriodFrom { get; set; }
        public System.DateTime PeriodTo { get; set; }
        public int CompanyId { get; set; }

        public virtual Companies Companies { get; set; }
    }
}