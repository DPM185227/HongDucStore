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
    public class MauSacController : Controller
    {
        private XeMayHongDucEntities db = new XeMayHongDucEntities();

        // GET: Admin/MauSac
        public ActionResult Index(int? idProduct)
        {
            if (idProduct != null) Session["idProduct"] = idProduct;
            var mauSac = db.MauSac.Include(m => m.SanPham).Where(m=>m.MaSanPham == idProduct);
            return View(mauSac.ToList());
        }

        // GET: Admin/MauSac/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MauSac mauSac = db.MauSac.Find(id);
            if (mauSac == null)
            {
                return HttpNotFound();
            }
            return View(mauSac);
        }

        // GET: Admin/MauSac/Create
        public ActionResult Create()
        {
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham");
            return View();
        }

        // POST: Admin/MauSac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMau,TenMau,HinhAnh,Delta,SoLuong,isCheck,MaSanPham,imageProductColor")] MauSac mauSac)
        {
            if (ModelState.IsValid)
            {
                if (mauSac.imageProductColor != null) mauSac.HinhAnh = Library.Library.UploadFile("ProductColor", mauSac.imageProductColor);
                mauSac.MaSanPham = Convert.ToInt32(Session["idProduct"]);
                if (db.MauSac.Count() == 0)
                {
                    mauSac.isCheck = 0;
                }
                else mauSac.isCheck = 1;
                db.MauSac.Add(mauSac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", mauSac.MaSanPham);
            return View(mauSac);
        }

        // GET: Admin/MauSac/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MauSac mauSac = db.MauSac.Find(id);
            if (mauSac == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", mauSac.MaSanPham);
            return View(mauSac);
        }

        // POST: Admin/MauSac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMau,TenMau,HinhAnh,Delta,SoLuong,isCheck,MaSanPham,imageProductColor")] MauSac mauSac)
        {
            if (ModelState.IsValid)
            {
                if (mauSac.imageProductColor != null) mauSac.HinhAnh = Library.Library.UploadFile("ProductColor", mauSac.imageProductColor);
                db.Entry(mauSac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", mauSac.MaSanPham);
            return View(mauSac);
        }

        // GET: Admin/MauSac/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MauSac mauSac = db.MauSac.Find(id);
            if (mauSac == null)
            {
                return HttpNotFound();
            }
            return View(mauSac);
        }

        // POST: Admin/MauSac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MauSac mauSac = db.MauSac.Find(id);
            db.MauSac.Remove(mauSac);
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

        [HttpGet]
        public ActionResult isCheck(int maMau,int maSanPham)
        {
            var getItemColor = db.MauSac.Where(m => m.MaSanPham == maSanPham).ToList();
            if (getItemColor.Count() > 1)
            {
                foreach (var item in getItemColor)
                {
                    if (item.MaMau == maMau)
                    {
                        item.isCheck = item.isCheck == 0 ? item.isCheck = 1 : item.isCheck = 0;
                    }
                    else
                        item.isCheck = 1;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Json(new { code = 200, msg = "Cập nhật thành công" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { code = 500, msg = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
            
        }
    }
}
