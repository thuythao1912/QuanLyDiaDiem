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
    
    public class HINHSANPHAMsController : BaseController
    {
        private QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();

        // GET: Admin/HINHSANPHAMs
        public ActionResult Index(int ? page)
        {
            var hINHSANPHAMs = db.HINHSANPHAMs.Include(h => h.SANPHAM).OrderBy(h=>h.HSP_ID).ToPagedList(page ?? 1,5);
            return View(hINHSANPHAMs);
        }

        // GET: Admin/HINHSANPHAMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHSANPHAM hINHSANPHAM = db.HINHSANPHAMs.Find(id);
            if (hINHSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(hINHSANPHAM);
        }

        // GET: Admin/HINHSANPHAMs/Create
        public ActionResult Create()
        {
            ViewBag.SANPHAM_ID = new SelectList(db.SANPHAMs, "SANPHAM_ID", "SANPHAM_TEN");
            return View();
        }

        // POST: Admin/HINHSANPHAMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HSP_ID,SANPHAM_ID,HSP_LINK")] HINHSANPHAM hINHSANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.HINHSANPHAMs.Add(hINHSANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SANPHAM_ID = new SelectList(db.SANPHAMs, "SANPHAM_ID", "SANPHAM_TEN", hINHSANPHAM.SANPHAM_ID);
            return View(hINHSANPHAM);
        }

        // GET: Admin/HINHSANPHAMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHSANPHAM hINHSANPHAM = db.HINHSANPHAMs.Find(id);
            if (hINHSANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.SANPHAM_ID = new SelectList(db.SANPHAMs, "SANPHAM_ID", "SANPHAM_TEN", hINHSANPHAM.SANPHAM_ID);
            return View(hINHSANPHAM);
        }

        // POST: Admin/HINHSANPHAMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HSP_ID,SANPHAM_ID,HSP_LINK")] HINHSANPHAM hINHSANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hINHSANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SANPHAM_ID = new SelectList(db.SANPHAMs, "SANPHAM_ID", "SANPHAM_TEN", hINHSANPHAM.SANPHAM_ID);
            return View(hINHSANPHAM);
        }

        // GET: Admin/HINHSANPHAMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHSANPHAM hINHSANPHAM = db.HINHSANPHAMs.Find(id);
            if (hINHSANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(hINHSANPHAM);
        }

        // POST: Admin/HINHSANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                HINHSANPHAM hINHSANPHAM = db.HINHSANPHAMs.Find(id);
                db.HINHSANPHAMs.Remove(hINHSANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult lochinhsanpham(string strdiadiem)
        {
            if (strdiadiem == "")
            {
                var listSP = (from bd in db.HINHSANPHAMs select bd);
                return View(listSP);
            }
            else
            {
                var listSP = (from bd in db.HINHSANPHAMs where bd.SANPHAM.SANPHAM_TEN == strdiadiem select bd);
                return View(listSP);
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
