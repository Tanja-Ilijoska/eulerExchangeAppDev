//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EulerExchangeAppDev.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImageURL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImageURL()
        {
            this.JewelryItems = new HashSet<JewelryItems>();
        }
    
        public int Id { get; set; }
        public string ImageURL1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JewelryItems> JewelryItems { get; set; }
    }
}
