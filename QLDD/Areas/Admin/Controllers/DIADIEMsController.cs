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
    
    public class DIADIEMsController : BaseController
    {
        private QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();

        // GET: Admin/DIADIEMs
        public ActionResult Index(int ? page)
        {
            //List<DIADIEM> listdiadiem;
            var dIADIEMs = db.DIADIEMs.Include(d => d.KHACHHANG).Include(d => d.LOAI).OrderBy(d=>d.DIADIEM_TEN).ToPagedList(page ?? 1,5);
            return View(dIADIEMs);
            //listdiadiem = (from dd in db.DIADIEMs join hdd in db.HINHDIADIEMs on dd.DIADIEM_ID equals hdd.DIADIEM_ID select dd).ToList();
            //return View(listdiadiem);

        }

        // GET: Admin/DIADIEMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIADIEM dIADIEM = db.DIADIEMs.Find(id);
            if (dIADIEM == null)
            {
                return HttpNotFound();
            }
            return View(dIADIEM);
        }

        // GET: Admin/DIADIEMs/Create
        public ActionResult Create()
        {
            ViewBag.KHACHHANG_ID = new SelectList(db.KHACHHANGs, "KHACHHANG_ID", "KHACHHANG_TEN");
            ViewBag.LOAI_ID = new SelectList(db.LOAIs, "LOAI_ID", "LOAI_TEN");
            return View();
        }

        // POST: Admin/DIADIEMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DIADIEM_ID,KHACHHANG_ID,LOAI_ID,DIADIEM_TEN,DIADIEM_DIACHI,DIADIEM_WEB,DIADIEM_MOTA")] DIADIEM dIADIEM)
        {
            if (ModelState.IsValid)
            {
                db.DIADIEMs.Add(dIADIEM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KHACHHANG_ID = new SelectList(db.KHACHHANGs, "KHACHHANG_ID", "KHACHHANG_TEN", dIADIEM.KHACHHANG_ID);
            ViewBag.LOAI_ID = new SelectList(db.LOAIs, "LOAI_ID", "LOAI_TEN", dIADIEM.LOAI_ID);
            return View(dIADIEM);
        }

        // GET: Admin/DIADIEMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIADIEM dIADIEM = db.DIADIEMs.Find(id);
            if (dIADIEM == null)
            {
                return HttpNotFound();
            }
            ViewBag.KHACHHANG_ID = new SelectList(db.KHACHHANGs, "KHACHHANG_ID", "KHACHHANG_TEN", dIADIEM.KHACHHANG_ID);
            ViewBag.LOAI_ID = new SelectList(db.LOAIs, "LOAI_ID", "LOAI_TEN", dIADIEM.LOAI_ID);
            return View(dIADIEM);
        }

        // POST: Admin/DIADIEMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DIADIEM_ID,KHACHHANG_ID,LOAI_ID,DIADIEM_TEN,DIADIEM_DIACHI,DIADIEM_WEB,DIADIEM_MOTA")] DIADIEM dIADIEM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dIADIEM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KHACHHANG_ID = new SelectList(db.KHACHHANGs, "KHACHHANG_ID", "KHACHHANG_TEN", dIADIEM.KHACHHANG_ID);
            ViewBag.LOAI_ID = new SelectList(db.LOAIs, "LOAI_ID", "LOAI_TEN", dIADIEM.LOAI_ID);
            return View(dIADIEM);
        }

        // GET: Admin/DIADIEMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIADIEM dIADIEM = db.DIADIEMs.Find(id);
            if (dIADIEM == null)
            {
                return HttpNotFound();
            }
            return View(dIADIEM);
        }

        // POST: Admin/DIADIEMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                DIADIEM dIADIEM = db.DIADIEMs.Find(id);
                db.DIADIEMs.Remove(dIADIEM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult SearchDanhSachDiaDiem(string searchString)
        {
            if (searchString == null)
            {
                var dsDiaDiem = (from b in db.DIADIEMs select b);
                return View(dsDiaDiem);
            }
            else
            {
                var dsDiaDiem = (from b in db.DIADIEMs select b).Where(x => x.DIADIEM_TEN.Contains(searchString));
                return View(dsDiaDiem);
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
