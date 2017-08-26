using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models.ViewModels
{
    public class StoreFilterItemViewModel
    {
        [DisplayName("Carat")]
        public float? carat { get; set; }
        [DisplayName("Weight")]
        public float? weightMin { get; set; }
        public float? weightMax { get; set; }


    }
}