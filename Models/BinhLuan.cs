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
    
    public partial class BinhLuan
    {
        public int IDComment { get; set; }
        public string NoiDung { get; set; }
        public Nullable<System.DateTime> TaoLuc { get; set; }
        public Nullable<int> TinhTrang { get; set; }
        public Nullable<int> MaSanPham { get; set; }
        public int MaKhachHang { get; set; }
        public Nullable<int> MaBaiViet { get; set; }
    
        public virtual BaiViet BaiViet { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
