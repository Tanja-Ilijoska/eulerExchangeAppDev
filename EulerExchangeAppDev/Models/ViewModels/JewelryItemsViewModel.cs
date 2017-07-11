using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models.ViewModels
{
    public class JewelryItemsViewModel
    {
        public int Id { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Millem { get; set; }
        public Nullable<decimal> Carat { get; set; }
        public Nullable<decimal> Pieces { get; set; }
        public string Size { get; set; }
        public Nullable<decimal> Length { get; set; }
        public Nullable<decimal> Thick { get; set; }
        public Nullable<decimal> Wide { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }


        public int CompanyId { get; set; }
        public System.Web.Mvc.SelectList CompanyList;
        public virtual Companies Companies { get; set; }
        [Display(Name = "Category")]
        public int CategoryJewelryId { get; set; }
        public System.Web.Mvc.SelectList CategoryJewelryList;
        public virtual JewelryCategoriesViewModel JewelryCategories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gemstones> Gemstones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JewelryItemsImageURL> JewelryItemsImageURL { get; set; }


        public int Quantity { get; set; }


    }
}