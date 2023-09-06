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
    public class DatHangController : Controller
    {
        private XeMayHongDucEntities db = new XeMayHongDucEntities();

        // GET: Admin/DatHang
        public ActionResult Index()
        {
            var datHang = db.DatHang.Include(d => d.ChiNhanh).Include(d => d.KhachHang);
            return View(datHang.ToList());
        }

        // GET: Admin/DatHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang datHang = db.DatHang.Find(id);
            if (datHang == null)
            {
                return HttpNotFound();
            }
            return View(datHang);
        }

        // GET: Admin/DatHang/Create
        public ActionResult Create()
        {
            ViewBag.MaChiNhanh = new SelectList(db.ChiNhanh, "MaChiNhanh", "TenChiNhanh");
            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang");
            return View();
        }

        // POST: Admin/DatHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDatHang,MaKhachHang,MaChiNhanh,ThoiGianNhanHang,HoTenNguoiNhan,SoDienThoai,TongTien,TinhTrangDonHang,NgayLap,NgayCapNhat")] DatHang datHang)
        {
            if (ModelState.IsValid)
            {
                db.DatHang.Add(datHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaChiNhanh = new SelectList(db.ChiNhanh, "MaChiNhanh", "TenChiNhanh", datHang.MaChiNhanh);
            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang", datHang.MaKhachHang);
            return View(datHang);
        }

        // GET: Admin/DatHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang datHang = db.DatHang.Find(id);
            if (datHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChiNhanh = new SelectList(db.ChiNhanh, "MaChiNhanh", "TenChiNhanh", datHang.MaChiNhanh);
            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang", datHang.MaKhachHang);
            return View(datHang);
        }

        // POST: Admin/DatHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDatHang,MaKhachHang,MaChiNhanh,ThoiGianNhanHang,HoTenNguoiNhan,SoDienThoai,TongTien,TinhTrangDonHang,NgayLap,NgayCapNhat")] DatHang datHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaChiNhanh = new SelectList(db.ChiNhanh, "MaChiNhanh", "TenChiNhanh", datHang.MaChiNhanh);
            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang", datHang.MaKhachHang);
            return View(datHang);
        }

        // GET: Admin/DatHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang datHang = db.DatHang.Find(id);
            if (datHang == null)
            {
                return HttpNotFound();
            }
            return View(datHang);
        }

        // POST: Admin/DatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatHang datHang = db.DatHang.Find(id);
            db.DatHang.Remove(datHang);
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
