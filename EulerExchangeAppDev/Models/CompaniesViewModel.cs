using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models
{
    public class CompaniesViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Contact person name")]
        public string ContactPersonName { get; set; }

        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        [Display(Name = "Year founded")]
        public int YearFounded { get; set; }

        [Display(Name = "Number of employees")]
        public Nullable<int> NumberOfEmployees { get; set; }

        [Display(Name = "Yearly Revenue")]
        public Nullable<decimal> YearlyRevenue { get; set; }

        [Display(Name = "Address")]
        public string CompanyAddress { get; set; }

        [Display(Name = "Country")]
        public string CompanyCountry { get; set; }

        [Display(Name = "City")]
        public string CompanyCity { get; set; }

        [Display(Name = "Phone")]
        public string CompanyPhone { get; set; }

        [Display(Name = "Website")]
        public string CompanyWebsite { get; set; }

        [Display(Name = "Additional emails")]
        public string AdditionalEMails { get; set; }

        [Display(Name = "Location")]
        public string CompanyLocation { get; set; }

        [Display(Name ="Company types")]
        public List<CompanyTypeViewModel> CompanyTypes { get; set; }
    }

    public class RingsItemDetails
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