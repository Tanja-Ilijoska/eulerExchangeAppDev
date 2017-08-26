using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models.ViewModels
{
    public class OrderViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public System.DateTime DateTime { get; set; }
        public int CustomerId { get; set; }
        public int SupplierId { get; set; }
        public Nullable<int> DiscountId { get; set; }
        public Nullable<int> PromotionId { get; set; }

        public virtual Companies Companies { get; set; } //customer
        public virtual Companies Companies1 { get; set; } //supplier
        public virtual Discounts Discounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderJewelryItems> OrderJewelryItems { get; set; }
        public virtual Promotions Promotions { get; set; }
    }
}