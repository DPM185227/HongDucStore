//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HongDucStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class HangXe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HangXe()
        {
            this.SanPham = new HashSet<SanPham>();
        }
    
        public int MaHang { get; set; }
        public string TenHang { get; set; }
        public string Banner { get; set; }

        public HttpPostedFileBase bannerFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPham { get; set; }
    }
}
