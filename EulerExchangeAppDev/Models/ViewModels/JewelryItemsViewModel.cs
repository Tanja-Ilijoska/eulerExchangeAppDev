using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models.ViewModels
{
    public class JewelryItemsViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public Nullable<float> Weight { get; set; }
        public Nullable<float> Price { get; set; }
        public Nullable<float> Millem { get; set; }
        public Nullable<float> Carat { get; set; }
        public Nullable<int> Pieces { get; set; }
        public string Size { get; set; }
        public Nullable<float> Length { get; set; }
        public Nullable<float> Thick { get; set; }
        public Nullable<float> Wide { get; set; }
        public string Comment { get; set; }


        public int CompanyId { get; set; }
        public System.Web.Mvc.SelectList CompanyList;
        public virtual Companies Companies { get; set; }
        [Display(Name = "Category")]
        public int CategoryJewelryId { get; set; }
        public System.Web.Mvc.SelectList CategoryJewelryList;
        public virtual JewelryCategories JewelryCategories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gemstones> Gemstones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JewelryItemsImageURL> JewelryItemsImageURL { get; set; }


    }
}