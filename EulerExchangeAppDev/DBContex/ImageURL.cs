//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EulerExchangeAppDev.DBContex
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImageURL
    {
        public ImageURL()
        {
            this.RingImageURL = new HashSet<RingImageURL>();
        }
    
        public int Id { get; set; }
        public Nullable<int> Type { get; set; }
        public string ImageURL1 { get; set; }
    
        public virtual ICollection<RingImageURL> RingImageURL { get; set; }
    }
}
