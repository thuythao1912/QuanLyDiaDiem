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
    
    public class HINHDIADIEMsController : BaseController
    {
        private QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();

        // GET: Admin/HINHDIADIEMs
        public ActionResult Index(int ? page)
        {
            var hINHDIADIEMs = db.HINHDIADIEMs.Include(h => h.DIADIEM).OrderBy(h=>h.HDD_ID).ToPagedList(page ?? 1,5);
            return View(hINHDIADIEMs);
        }

        // GET: Admin/HINHDIADIEMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHDIADIEM hINHDIADIEM = db.HINHDIADIEMs.Find(id);
            if (hINHDIADIEM == null)
            {
                return HttpNotFound();
            }
            return View(hINHDIADIEM);
        }

        // GET: Admin/HINHDIADIEMs/Create
        public ActionResult Create()
        {
            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "KHACHHANG_ID");
            return View();
        }

        // POST: Admin/HINHDIADIEMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HDD_ID,DIADIEM_ID,HDD_LINK")] HINHDIADIEM hINHDIADIEM)
        {
            if (ModelState.IsValid)
            {
                db.HINHDIADIEMs.Add(hINHDIADIEM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "KHACHHANG_ID", hINHDIADIEM.DIADIEM_ID);
            return View(hINHDIADIEM);
        }

        // GET: Admin/HINHDIADIEMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHDIADIEM hINHDIADIEM = db.HINHDIADIEMs.Find(id);
            if (hINHDIADIEM == null)
            {
                return HttpNotFound();
            }
            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "KHACHHANG_ID", hINHDIADIEM.DIADIEM_ID);
            return View(hINHDIADIEM);
        }

        // POST: Admin/HINHDIADIEMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HDD_ID,DIADIEM_ID,HDD_LINK")] HINHDIADIEM hINHDIADIEM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hINHDIADIEM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "KHACHHANG_ID", hINHDIADIEM.DIADIEM_ID);
            return View(hINHDIADIEM);
        }

        // GET: Admin/HINHDIADIEMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HINHDIADIEM hINHDIADIEM = db.HINHDIADIEMs.Find(id);
            if (hINHDIADIEM == null)
            {
                return HttpNotFound();
            }
            return View(hINHDIADIEM);
        }

        // POST: Admin/HINHDIADIEMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                HINHDIADIEM hINHDIADIEM = db.HINHDIADIEMs.Find(id);
                db.HINHDIADIEMs.Remove(hINHDIADIEM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult lochinhdiadiem(string strdiadiem)
        {
            if (strdiadiem == "")
            {
                var listSP = (from bd in db.HINHDIADIEMs select bd);
                return View(listSP);
            }
            else
            {
                var listSP = (from bd in db.HINHDIADIEMs where bd.DIADIEM.DIADIEM_TEN == strdiadiem select bd);
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
