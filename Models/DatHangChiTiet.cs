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
    
    public partial class DatHangChiTiet
    {
        public int STT { get; set; }
        public Nullable<int> MaSanPham { get; set; }
        public Nullable<int> MaMau { get; set; }
        public Nullable<int> GiaBan { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<int> TongTien { get; set; }
        public Nullable<int> MaDatHang { get; set; }
    
        public virtual MauSac MauSac { get; set; }
        public virtual SanPham SanPham { get; set; }
        public virtual DatHang DatHang { get; set; }
    }
}