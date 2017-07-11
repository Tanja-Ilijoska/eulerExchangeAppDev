using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models
{
    public class JewelryMachinesViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string ModelType { get; set; }
        public string BrandName { get; set; }
        public string ProductDetails { get; set; }
        public string AfterSalesService { get; set; }
        public Nullable<decimal> Price { get; set; }
        public virtual ICollection<JewelryMachinesImageURL> JewelryMachinesImageURL { get; set; }
        public int CompanyId { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public virtual Companies Companies { get; set; }
    }

    public class JewelryMachinesListItem
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string ModelType { get; set; }
        public string BrandName { get; set; }
        public string ProductDetails { get; set; }
        public string AfterSalesService { get; set; }
        public Nullable<decimal> Price { get; set; }
        public virtual ICollection<JewelryMachinesImageURL> JewelryMachinesImageURL { get; set; }
        public int CompanyId { get; set; }
    }
}