using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models.ViewModels
{
    public class JewelryCategoriesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<JewelryItems> JewelryItems { get; set; }

        public string getNameUnderScored()
        {
            return Name.Replace(' ', '_');
        }
    }
}