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
    public class HangXeController : Controller
    {
        private XeMayHongDucEntities db = new XeMayHongDucEntities();

        // GET: Admin/HangXe
        public ActionResult Index()
        {
            return View(db.HangXe.ToList());
        }

        // GET: Admin/HangXe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangXe hangXe = db.HangXe.Find(id);
            if (hangXe == null)
            {
                return HttpNotFound();
            }
            return View(hangXe);
        }

        // GET: Admin/HangXe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HangXe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHang,TenHang,Banner,bannerFile")] HangXe hangXe)
        {
            if (ModelState.IsValid)
            {
                if (hangXe.bannerFile != null) hangXe.Banner = Library.Library.UploadFile("HangXe", hangXe.bannerFile);
                db.HangXe.Add(hangXe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hangXe);
        }

        // GET: Admin/HangXe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangXe hangXe = db.HangXe.Find(id);
            if (hangXe == null)
            {
                return HttpNotFound();
            }
            return View(hangXe);
        }

        // POST: Admin/HangXe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHang,TenHang,Banner,bannerFile")] HangXe hangXe)
        {
            if (ModelState.IsValid)
            {
                if (hangXe.bannerFile != null)
                {
                    Library.Library.DeleteFile(hangXe.Banner);
                    hangXe.Banner = Library.Library.UploadFile("HangXe", hangXe.bannerFile);
                }
                db.Entry(hangXe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hangXe);
        }

        // GET: Admin/HangXe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangXe hangXe = db.HangXe.Find(id);
            if (hangXe == null)
            {
                return HttpNotFound();
            }
            return View(hangXe);
        }

        // POST: Admin/HangXe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HangXe hangXe = db.HangXe.Find(id);
            db.HangXe.Remove(hangXe);
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
