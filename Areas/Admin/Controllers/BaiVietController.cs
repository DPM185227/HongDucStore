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
    public class BaiVietController : Controller
    {
        private XeMayHongDucEntities db = new XeMayHongDucEntities();

        // GET: Admin/BaiViet
        public ActionResult Index()
        {
            var baiViet = db.BaiViet.Include(b => b.NhanVien);
            return View(baiViet.ToList());
        }

        // GET: Admin/BaiViet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViet.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }

        // GET: Admin/BaiViet/Create
        public ActionResult Create()
        {
            ViewBag.MaNhanVien = new SelectList(db.NhanVien, "MaNhanVien", "TenNhanVien");
            return View();
        }

        // POST: Admin/BaiViet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "MaBaiViet,TenBaiViet,MoTaNgan,NoiDung,LuotXem,XetDuyet,MaNhanVien,HinhAnh,imagePost")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                if (baiViet.imagePost != null) baiViet.HinhAnh = Library.Library.UploadFile("Post", baiViet.imagePost);
                baiViet.LuotXem = 0;
                baiViet.XetDuyet = 0;
                baiViet.MaNhanVien = Convert.ToInt32(Session["maNhanVien"]);
                db.BaiViet.Add(baiViet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNhanVien = new SelectList(db.NhanVien, "MaNhanVien", "TenNhanVien", baiViet.MaNhanVien);
            return View(baiViet);
        }

        // GET: Admin/BaiViet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViet.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNhanVien = new SelectList(db.NhanVien, "MaNhanVien", "TenNhanVien", baiViet.MaNhanVien);
            return View(baiViet);
        }

        // POST: Admin/BaiViet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "MaBaiViet,TenBaiViet,MoTaNgan,NoiDung,LuotXem,XetDuyet,MaNhanVien,HinhAnh,imagePost")] BaiViet baiViet)
        {
            if (ModelState.IsValid)
            {
                if (baiViet.imagePost != null) baiViet.HinhAnh = Library.Library.UploadFile("Post", baiViet.imagePost);
                db.Entry(baiViet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNhanVien = new SelectList(db.NhanVien, "MaNhanVien", "TenNhanVien", baiViet.MaNhanVien);
            return View(baiViet);
        }

        // GET: Admin/BaiViet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiViet baiViet = db.BaiViet.Find(id);
            if (baiViet == null)
            {
                return HttpNotFound();
            }
            return View(baiViet);
        }

        // POST: Admin/BaiViet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaiViet baiViet = db.BaiViet.Find(id);
            db.BaiViet.Remove(baiViet);
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
        public JsonResult isCheck(int idBaiViet)
        {
            var getPost = db.BaiViet.Where(m => m.MaBaiViet == idBaiViet).FirstOrDefault();
            if (getPost != null)
            {
                 getPost.XetDuyet = getPost.XetDuyet == 1 ?   getPost.XetDuyet = 0 :   getPost.XetDuyet = 1;
                db.Entry(getPost).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                    return Json(new { code = 200, msg = "Cập nhật thành công" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { code = 500, msg = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { code = 500, msg = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);

        }
    }
}
