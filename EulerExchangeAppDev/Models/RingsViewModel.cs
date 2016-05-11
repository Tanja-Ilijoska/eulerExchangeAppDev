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

        public string Number { get; set; }
        public string Decription { get; set; }
        public string Size { get; set; }
        public Nullable<decimal> Radius { get; set; }
        public Nullable<decimal> Circumference { get; set; }
        public Nullable<int> GemstoneID { get; set; }
        public Nullable<int> ImageURLID { get; set; }
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
        public Nullable<int> GemstoneID { get; set; }
        public Nullable<int> ImageURLID { get; set; }
    }
}