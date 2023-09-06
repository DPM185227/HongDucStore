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
    public class LienHeBanXeController : Controller
    {
        private XeMayHongDucEntities db = new XeMayHongDucEntities();

        // GET: Admin/LienHeBanXe
        public ActionResult Index()
        {
            return View(db.LienHeBanXe.ToList());
        }

        // GET: Admin/LienHeBanXe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHeBanXe lienHeBanXe = db.LienHeBanXe.Find(id);
            if (lienHeBanXe == null)
            {
                return HttpNotFound();
            }
            return View(lienHeBanXe);
        }

        // GET: Admin/LienHeBanXe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LienHeBanXe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLienHe,HoVaTen,DiaChi,SoDienThoai,TenXe,NamSanXuat,MoTaXe,HinhAnhXe,GiaTienMuonBan")] LienHeBanXe lienHeBanXe)
        {
            if (ModelState.IsValid)
            {
                db.LienHeBanXe.Add(lienHeBanXe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lienHeBanXe);
        }

        // GET: Admin/LienHeBanXe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHeBanXe lienHeBanXe = db.LienHeBanXe.Find(id);
            if (lienHeBanXe == null)
            {
                return HttpNotFound();
            }
            return View(lienHeBanXe);
        }

        // POST: Admin/LienHeBanXe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLienHe,HoVaTen,DiaChi,SoDienThoai,TenXe,NamSanXuat,MoTaXe,HinhAnhXe,GiaTienMuonBan")] LienHeBanXe lienHeBanXe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lienHeBanXe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lienHeBanXe);
        }

        // GET: Admin/LienHeBanXe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHeBanXe lienHeBanXe = db.LienHeBanXe.Find(id);
            if (lienHeBanXe == null)
            {
                return HttpNotFound();
            }
            return View(lienHeBanXe);
        }

        // POST: Admin/LienHeBanXe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LienHeBanXe lienHeBanXe = db.LienHeBanXe.Find(id);
            db.LienHeBanXe.Remove(lienHeBanXe);
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
