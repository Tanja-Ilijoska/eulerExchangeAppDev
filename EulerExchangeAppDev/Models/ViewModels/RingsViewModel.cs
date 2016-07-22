using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models
{
    public class RingsViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string Number { get; set; }
        public string Decription { get; set; }
        public string Size { get; set; }
        public Nullable<decimal> Radius { get; set; }
        public Nullable<decimal> Circumference { get; set; }
        public Nullable<decimal> Carat { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public virtual ICollection<ImageURL> ImageURL { get; set; }
        public int CompanyId { get; set; }
    }

    public class RingsListItem
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string Number { get; set; }
        public string Decription { get; set; }
        public string Size { get; set; }
        public Nullable<decimal> Radius { get; set; }
        public Nullable<decimal> Circumference { get; set; }
        public Nullable<decimal> Carat { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<int> ImageURLID { get; set; }
    }
}