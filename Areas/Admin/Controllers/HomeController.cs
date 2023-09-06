using HongDucStore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HongDucStore.Models.DataPoint;

namespace HongDucStore.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        XeMayHongDucEntities db = new XeMayHongDucEntities();
        public ActionResult Index()
        {
            ViewBag.dataOrder = db.DatHang.ToList();
            ViewBag.sanPham = db.SanPham.ToList();
            ViewBag.Color = db.MauSac.ToList();
            ViewBag.datHangChiTiet = db.DatHangChiTiet.ToList();
            List<DataPoint> listData = new List<DataPoint>();
            foreach(var item in db.DatHang.Where(m=>m.TinhTrangDonHang == 4).ToList())
            {
                listData.Add(new DataPoint(item.KhachHang.TenKhachHang.ToString(),Convert.ToDouble(item.TongTien)));
            }
            ViewBag.dataPoints = JsonConvert.SerializeObject(listData);
            return View();
        }

        // get login
        [HttpGet]
        public ActionResult LoginAdmin()
        {
            return View();
        }

        // post login
        [HttpPost]
        public ActionResult LoginAdmin(NhanVien nhanVien)
        {
            string matKhau = Library.Library.ComputeMd5Hash(nhanVien.MatKhau);
            var nhanVienLogin = db.NhanVien.Where(m=>m.TenDangNhap == nhanVien.TenDangNhap && m.MatKhau == matKhau).FirstOrDefault();
            if(nhanVienLogin != null)
            {
                Session["maNhanVien"] = nhanVienLogin.MaNhanVien;
                Session["tenNhanVien"] = nhanVienLogin.TenNhanVien;
                Session["hinhAnh"] = nhanVienLogin.HinhAnh;
                Session["quyen"] = nhanVienLogin.LoaiTaiKhoan;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorLogin = "Đăng nhập thất bại!!!.Vui lòng thử lại";
                return View(nhanVien);
            }
            
        }

        // Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("LoginAdmin", "Home");
        }
    }
}