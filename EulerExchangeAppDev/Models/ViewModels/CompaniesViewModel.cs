using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models.ViewModels
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
        [Display(Name = "Logo")]
        public string ImageURL { get; set; }

        [Display(Name ="Company types")]
        public List<CheckBoxListItem> CompanyTypes { get; set; }

        public CompaniesViewModel()
        {
            CompanyTypes = new List<CheckBoxListItem>();
        }
    }   
}