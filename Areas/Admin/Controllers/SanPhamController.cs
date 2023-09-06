using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HongDucStore.Models;

namespace HongDucStore.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        private XeMayHongDucEntities db = new XeMayHongDucEntities();

        // GET: Admin/SanPham
        public ActionResult Index()
        {
            var sanPham = db.SanPham.Include(s => s.ChiNhanh).Include(s => s.DanhMuc).Include(s => s.HangXe).Include(s => s.NhanVien);
            return View(sanPham.ToList());
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPham/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaChiNhanh = new SelectList(db.ChiNhanh, "MaChiNhanh", "TenChiNhanh");
            ViewBag.MaDanhMuc = new SelectList(db.DanhMuc, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaHang = new SelectList(db.HangXe, "MaHang", "TenHang");
            ViewBag.MaNhanVien = new SelectList(db.NhanVien, "MaNhanVien", "TenNhanVien");
            return View();
        }

        // POST: Admin/SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "MaSanPham,TenSanPham,GiaBan,GiaGiam,MoTa,KhuyenMai,MaChiNhanh,MaHang,MaDanhMuc,MaNhanVien,HinhAnh,imageProduct")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                if (sanPham.imageProduct != null) sanPham.HinhAnh = Library.Library.UploadFile("Product", sanPham.imageProduct);
                sanPham.MaNhanVien = Convert.ToInt32(Session["maNhanVien"]);
                db.SanPham.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaChiNhanh = new SelectList(db.ChiNhanh, "MaChiNhanh", "TenChiNhanh", sanPham.MaChiNhanh);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMuc, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaHang = new SelectList(db.HangXe, "MaHang", "TenHang", sanPham.MaHang);
            ViewBag.MaNhanVien = new SelectList(db.NhanVien, "MaNhanVien", "TenNhanVien", sanPham.MaNhanVien);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChiNhanh = new SelectList(db.ChiNhanh, "MaChiNhanh", "TenChiNhanh", sanPham.MaChiNhanh);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMuc, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaHang = new SelectList(db.HangXe, "MaHang", "TenHang", sanPham.MaHang);
            ViewBag.MaNhanVien = new SelectList(db.NhanVien, "MaNhanVien", "TenNhanVien", sanPham.MaNhanVien);
            return View(sanPham);
        }

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "MaSanPham,TenSanPham,GiaBan,GiaGiam,MoTa,KhuyenMai,MaChiNhanh,MaHang,MaDanhMuc,MaNhanVien,HinhAnh,imageProduct")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                sanPham.MaNhanVien = Convert.ToInt32(Session["maNhanVien"]);
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaChiNhanh = new SelectList(db.ChiNhanh, "MaChiNhanh", "TenChiNhanh", sanPham.MaChiNhanh);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMuc, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaHang = new SelectList(db.HangXe, "MaHang", "TenHang", sanPham.MaHang);
            ViewBag.MaNhanVien = new SelectList(db.NhanVien, "MaNhanVien", "TenNhanVien", sanPham.MaNhanVien);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPham.Find(id);
            db.SanPham.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
