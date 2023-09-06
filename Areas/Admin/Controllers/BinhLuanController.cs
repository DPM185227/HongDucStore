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
    public class BinhLuanController : Controller
    {
        private XeMayHongDucEntities db = new XeMayHongDucEntities();

        // GET: Admin/BinhLuan
        public ActionResult Index(int ?idPost, int ?idProduct)
        {
            if(idPost != null)
            {
                var binhLuan = db.BinhLuan.Include(b => b.BaiViet).Include(b => b.KhachHang).Include(b => b.SanPham);
                return View(binhLuan.Where(m=>m.MaBaiViet == idPost).ToList());
            }
            else if(idProduct != null)
            {
                var binhLuan = db.BinhLuan.Include(b => b.BaiViet).Include(b => b.KhachHang).Include(b => b.SanPham);
                return View(binhLuan.Where(m => m.MaSanPham == idProduct).ToList());
            }
            return HttpNotFound();
        }

        // GET: Admin/BinhLuan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // GET: Admin/BinhLuan/Create
        public ActionResult Create()
        {
            ViewBag.MaBaiViet = new SelectList(db.BaiViet, "MaBaiViet", "TenBaiViet");
            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang");
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham");
            return View();
        }

        // POST: Admin/BinhLuan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDComment,NoiDung,TaoLuc,TinhTrang,MaSanPham,MaKhachHang,MaBaiViet")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.BinhLuan.Add(binhLuan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaBaiViet = new SelectList(db.BaiViet, "MaBaiViet", "TenBaiViet", binhLuan.MaBaiViet);
            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang", binhLuan.MaKhachHang);
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", binhLuan.MaSanPham);
            return View(binhLuan);
        }

        // GET: Admin/BinhLuan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBaiViet = new SelectList(db.BaiViet, "MaBaiViet", "TenBaiViet", binhLuan.MaBaiViet);
            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang", binhLuan.MaKhachHang);
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", binhLuan.MaSanPham);
            return View(binhLuan);
        }

        // POST: Admin/BinhLuan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDComment,NoiDung,TaoLuc,TinhTrang,MaSanPham,MaKhachHang,MaBaiViet")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binhLuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBaiViet = new SelectList(db.BaiViet, "MaBaiViet", "TenBaiViet", binhLuan.MaBaiViet);
            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang", binhLuan.MaKhachHang);
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", binhLuan.MaSanPham);
            return View(binhLuan);
        }

        // GET: Admin/BinhLuan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // POST: Admin/BinhLuan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BinhLuan binhLuan = db.BinhLuan.Find(id);
            db.BinhLuan.Remove(binhLuan);
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
        public JsonResult isCheck(int idComment)
        {
            var getComment = db.BinhLuan.Where(m => m.IDComment == idComment).FirstOrDefault();
            if (getComment != null)
            {
                getComment.TinhTrang = getComment.TinhTrang == 0 ? getComment.TinhTrang = 1 : getComment.TinhTrang = 0;
                db.Entry(getComment).State = EntityState.Modified;
                if(db.SaveChanges() > 0)
                    return Json(new { code = 200, msg = "Cập nhật thành công" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { code = 500, msg = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { code = 500, msg = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);

        }
    }
}
