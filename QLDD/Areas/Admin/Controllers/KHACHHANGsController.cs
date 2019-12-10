using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLDD.Models;
using PagedList;
namespace QLDD.Areas.Admin.Controllers
{
    
    public class KHACHHANGsController : BaseController
    {
        private QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();

        // GET: Admin/KHACHHANGs
        public ActionResult Index(int ? page)
        {
            
            var kHACHHANGs = db.KHACHHANGs.OrderBy(d => d.KHACHHANG_ID).ToPagedList(page ?? 1, 5);
            return View(kHACHHANGs);
        }

        // GET: Admin/KHACHHANGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // GET: Admin/KHACHHANGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KHACHHANGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KHACHHANG_ID,KHACHHANG_TEN,KHACHHANG_SDT,KHACHHANG_EMAIL,KHACHHANG_PASSWORD")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.KHACHHANGs.Add(kHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHACHHANG);
        }

        // GET: Admin/KHACHHANGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: Admin/KHACHHANGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KHACHHANG_ID,KHACHHANG_TEN,KHACHHANG_SDT,KHACHHANG_EMAIL,KHACHHANG_PASSWORD")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHACHHANG);
        }

        // GET: Admin/KHACHHANGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: Admin/KHACHHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
                db.KHACHHANGs.Remove(kHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult SearchDanhSachKhachHang(string searchString)
        {
            if (searchString == null)
            {
                var dsKhachHang = (from b in db.KHACHHANGs select b);
                return View(dsKhachHang);
            }
            else
            {
                var dsKhachHang = (from b in db.KHACHHANGs select b).Where(x => x.KHACHHANG_TEN.Contains(searchString));
                return View(dsKhachHang);
            }
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
