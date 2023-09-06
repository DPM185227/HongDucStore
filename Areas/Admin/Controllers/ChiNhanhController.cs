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
    public class ChiNhanhController : Controller
    {
        private XeMayHongDucEntities db = new XeMayHongDucEntities();

        // GET: Admin/ChiNhanh
        public ActionResult Index()
        {
            return View(db.ChiNhanh.ToList());
        }

        // GET: Admin/ChiNhanh/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiNhanh chiNhanh = db.ChiNhanh.Find(id);
            if (chiNhanh == null)
            {
                return HttpNotFound();
            }
            return View(chiNhanh);
        }

        // GET: Admin/ChiNhanh/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ChiNhanh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChiNhanh,TenChiNhanh,SoDienThoai,DiaChiMap,MaTinh,MaHuyen")] ChiNhanh chiNhanh)
        {
            if (ModelState.IsValid)
            {
                if(chiNhanh.MaHuyen == 0 || chiNhanh.MaTinh == 0)
                {
                    ViewBag.ErrorSelect = "Vui lòng chọn đầy đủ tỉnh và huyện";
                    return View(chiNhanh);
                }
                db.ChiNhanh.Add(chiNhanh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chiNhanh);
        }

        // GET: Admin/ChiNhanh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiNhanh chiNhanh = db.ChiNhanh.Find(id);
            if (chiNhanh == null)
            {
                return HttpNotFound();
            }
            return View(chiNhanh);
        }

        // POST: Admin/ChiNhanh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int MaTinhOld,int MaHuyenOld,[Bind(Include = "MaChiNhanh,TenChiNhanh,SoDienThoai,DiaChiMap,MaTinh,MaHuyen")] ChiNhanh chiNhanh)
        {
            if (ModelState.IsValid)
            {
                if(chiNhanh.MaTinh == 0 || chiNhanh.MaHuyen == 0)
                {
                    chiNhanh.MaTinh = MaTinhOld;
                    chiNhanh.MaHuyen = MaHuyenOld;
                }
                db.Entry(chiNhanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chiNhanh);
        }

        // GET: Admin/ChiNhanh/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiNhanh chiNhanh = db.ChiNhanh.Find(id);
            if (chiNhanh == null)
            {
                return HttpNotFound();
            }
            return View(chiNhanh);
        }

        // POST: Admin/ChiNhanh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiNhanh chiNhanh = db.ChiNhanh.Find(id);
            db.ChiNhanh.Remove(chiNhanh);
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
