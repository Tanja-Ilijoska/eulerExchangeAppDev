using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models.ViewModels
{
    public class OrderJewelryItemsViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int JewelryItemId { get; set; }
        public int Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }

        public virtual JewelryItemsViewModel JewelryItems { get; set; }
        public virtual OrderViewModel Orders { get; set; }
    }
}