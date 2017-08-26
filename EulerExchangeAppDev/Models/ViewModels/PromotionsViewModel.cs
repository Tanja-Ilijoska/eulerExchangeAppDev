using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models
{
    public class PromotionsViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Millem { get; set; }
        [Display(Name = "Percent")]
        public Nullable<decimal> Persent { get; set; }
        public Nullable<decimal> MinOrderGram { get; set; }
        public Nullable<decimal> MinOrderPrice { get; set; }
        [Display(Name = "Period from")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public System.DateTime PeriodFrom { get; set; }
        [Display(Name = "Period to")]
        [DataType(DataType.Date)]
        public System.DateTime PeriodTo { get; set; }
        public bool Rings { get; set; }
        public bool EngagementRings { get; set; }
        public bool WeddingRings { get; set; }
        public bool NecklacesSets { get; set; }
        public bool NecklacesChains { get; set; }
        public bool ChildrenNecklaceChains { get; set; }
        public bool Sets { get; set; }
        public bool Bracelets { get; set; }
        public bool ChildrenBracelets { get; set; }
        public bool Bangles { get; set; }
        public bool ChildrenBangles { get; set; }
        public bool Earrings { get; set; }
        public bool ChildrenEarrings { get; set; }
        public bool Pendants { get; set; }
        public bool PinBrooch { get; set; }
        public bool Cufflinks { get; set; }
        public bool Watches { get; set; }
        public bool Sunglasses { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public int CompanyId { get; set; }

        public virtual Companies Companies { get; set; }
    }

    public class PromotionsListItem
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public Nullable<decimal> MinOrderGram { get; set; }
        public Nullable<decimal> MinOrderPrice { get; set; }
        public System.DateTime PeriodFrom { get; set; }
        public System.DateTime PeriodTo { get; set; }
        public Nullable<bool> Rings { get; set; }
        public Nullable<bool> EngagementRIngs { get; set; }
        public Nullable<bool> WeddingRings { get; set; }
        public Nullable<bool> Pendants { get; set; }
        public Nullable<bool> Chains { get; set; }
        public Nullable<bool> Necklaces { get; set; }
        public Nullable<bool> Bracelets { get; set; }
        public Nullable<bool> Earrings { get; set; }
        public Nullable<bool> LightSets { get; set; }
        public Nullable<bool> SetsWatchesSunglasses { get; set; }
        public int CompanyId { get; set; }

        public virtual Companies Companies { get; set; }
    }
}