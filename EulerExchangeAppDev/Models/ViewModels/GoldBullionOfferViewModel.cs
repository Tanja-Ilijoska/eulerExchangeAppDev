using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EulerExchangeAppDev.Models.ViewModels
{
    public class GoldBullionOfferViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Nullable<float> Price { get; set; }
        public Nullable<float> Weight { get; set; }
        public Nullable<float> Carat { get; set; }

        //  [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{HH:mm dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateTime { get; set; }

        public virtual Companies Companies { get; set; }
        
        public int getSecondsToEnd()
        {
            if(DateTime == null)
                return 0;
            TimeSpan diff = DateTime.Value.AddMinutes(5).Subtract(System.DateTime.Now);
            if(diff.TotalSeconds <= 0)
                return 0;
            return (int)Math.Floor(diff.TotalSeconds);
        }
    }

    public class GoldBullionOfferListItem
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Nullable<float> Price { get; set; }
        public Nullable<float> Weight { get; set; }
        public Nullable<float> Carat { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }

        public virtual Companies Companies { get; set; }
    }
}
