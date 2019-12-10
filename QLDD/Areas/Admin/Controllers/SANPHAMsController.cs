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
    
    public class SANPHAMsController : BaseController
    {
        private QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();

        // GET: Admin/SANPHAMs
        public ActionResult Index(int ? page)
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.DIADIEM).OrderBy(s=>s.SANPHAM_ID).ToPagedList(page ?? 1,5);
            return View(sANPHAMs);
        }

        // GET: Admin/SANPHAMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Create
        public ActionResult Create()
        {
            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "KHACHHANG_ID");
            return View();
        }

        // POST: Admin/SANPHAMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SANPHAM_ID,DIADIEM_ID,SANPHAM_TEN,SANPHAM_MOTA,SANPHAM_GIA")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "KHACHHANG_ID", sANPHAM.DIADIEM_ID);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "KHACHHANG_ID", sANPHAM.DIADIEM_ID);
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SANPHAM_ID,DIADIEM_ID,SANPHAM_TEN,SANPHAM_MOTA,SANPHAM_GIA")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "KHACHHANG_ID", sANPHAM.DIADIEM_ID);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                SANPHAM sANPHAM = db.SANPHAMs.Find(id);
                db.SANPHAMs.Remove(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult locdiadiem(int ?strdiadiem)
        {
            if (strdiadiem == null)
            {
                var listSP = (from bd in db.SANPHAMs select bd);
                return View(listSP);
            }
            else
            {
                var listSP = (from bd in db.SANPHAMs where bd.DIADIEM_ID == strdiadiem select bd);
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
