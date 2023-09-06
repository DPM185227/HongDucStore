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
    public class NhanVienController : Controller
    {
        private XeMayHongDucEntities db = new XeMayHongDucEntities();

        // GET: Admin/NhanVien
        public ActionResult Index()
        {
            return View(db.NhanVien.ToList());
        }

        // GET: Admin/NhanVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: Admin/NhanVien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNhanVien,TenNhanVien,DiaChi,SoDienThoai,CMT_CCCD,TenDangNhap,MatKhau,LoaiTaiKhoan,KichHoat,HinhAnh,staffFile")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                if (nhanVien.staffFile != null) nhanVien.HinhAnh = Library.Library.UploadFile("Staff", nhanVien.staffFile);
                nhanVien.MatKhau = Library.Library.ComputeMd5Hash(nhanVien.MatKhau);
                nhanVien.KichHoat = 0;
                db.NhanVien.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhanVien);
        }

        // GET: Admin/NhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string MatKhauCu,[Bind(Include = "MaNhanVien,TenNhanVien,DiaChi,SoDienThoai,CMT_CCCD,TenDangNhap,MatKhau,LoaiTaiKhoan,KichHoat,HinhAnh,staffFile")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                if(nhanVien.staffFile != null)
                {
                    Library.Library.DeleteFile(nhanVien.HinhAnh);
                    nhanVien.HinhAnh = Library.Library.UploadFile("Staff", nhanVien.staffFile);
                }
                if (nhanVien.MatKhau != null) nhanVien.MatKhau = Library.Library.ComputeMd5Hash(nhanVien.MatKhau);
                else nhanVien.MatKhau = MatKhauCu;
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanVien);
        }

        // GET: Admin/NhanVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanVien.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanVien = db.NhanVien.Find(id);
            Library.Library.DeleteFile(nhanVien.HinhAnh);
            db.NhanVien.Remove(nhanVien);
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
