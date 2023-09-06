using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HongDucStore.Models
{
    public class CartItem
    {
        public Guid IdItem { get; set; }
        public SanPham sanPham { get; set; }
        public int SoLuongMua { get; set; }
        public MauSac mauSac { get; set; } = null;
    }
}