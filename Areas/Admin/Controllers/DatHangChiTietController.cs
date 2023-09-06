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
    public class DatHangChiTietController : Controller
    {
        private XeMayHongDucEntities db = new XeMayHongDucEntities();

        // GET: Admin/DatHangChiTiet
        public ActionResult Index(int idOrder)
        {
            var datHangChiTiet = db.DatHangChiTiet.Include(d => d.MauSac).Include(d => d.SanPham).Include(d => d.DatHang);
            return View(datHangChiTiet.Where(m=>m.MaDatHang == idOrder).ToList());
        }

        // GET: Admin/DatHangChiTiet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHangChiTiet datHangChiTiet = db.DatHangChiTiet.Find(id);
            if (datHangChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(datHangChiTiet);
        }

        // GET: Admin/DatHangChiTiet/Create
        public ActionResult Create()
        {
            ViewBag.MaMau = new SelectList(db.MauSac, "MaMau", "TenMau");
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham");
            ViewBag.MaDatHang = new SelectList(db.DatHang, "MaDatHang", "ThoiGianNhanHang");
            return View();
        }

        // POST: Admin/DatHangChiTiet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "STT,MaSanPham,MaMau,GiaBan,SoLuong,TongTien,MaDatHang")] DatHangChiTiet datHangChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.DatHangChiTiet.Add(datHangChiTiet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaMau = new SelectList(db.MauSac, "MaMau", "TenMau", datHangChiTiet.MaMau);
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", datHangChiTiet.MaSanPham);
            ViewBag.MaDatHang = new SelectList(db.DatHang, "MaDatHang", "ThoiGianNhanHang", datHangChiTiet.MaDatHang);
            return View(datHangChiTiet);
        }

        // GET: Admin/DatHangChiTiet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHangChiTiet datHangChiTiet = db.DatHangChiTiet.Find(id);
            if (datHangChiTiet == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMau = new SelectList(db.MauSac, "MaMau", "TenMau", datHangChiTiet.MaMau);
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", datHangChiTiet.MaSanPham);
            ViewBag.MaDatHang = new SelectList(db.DatHang, "MaDatHang", "ThoiGianNhanHang", datHangChiTiet.MaDatHang);
            return View(datHangChiTiet);
        }

        // POST: Admin/DatHangChiTiet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "STT,MaSanPham,MaMau,GiaBan,SoLuong,TongTien,MaDatHang")] DatHangChiTiet datHangChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datHangChiTiet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaMau = new SelectList(db.MauSac, "MaMau", "TenMau", datHangChiTiet.MaMau);
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", datHangChiTiet.MaSanPham);
            ViewBag.MaDatHang = new SelectList(db.DatHang, "MaDatHang", "ThoiGianNhanHang", datHangChiTiet.MaDatHang);
            return View(datHangChiTiet);
        }

        // GET: Admin/DatHangChiTiet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHangChiTiet datHangChiTiet = db.DatHangChiTiet.Find(id);
            if (datHangChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(datHangChiTiet);
        }

        // POST: Admin/DatHangChiTiet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatHangChiTiet datHangChiTiet = db.DatHangChiTiet.Find(id);
            db.DatHangChiTiet.Remove(datHangChiTiet);
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
