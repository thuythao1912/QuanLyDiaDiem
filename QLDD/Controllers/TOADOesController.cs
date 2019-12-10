using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLDD.Models;

namespace QLDD.Controllers
{
    public class TOADOesController : Controller
    {
        private QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();

        // GET: TOADOes
        public ActionResult Index()
        {
            var tOADOes = db.TOADOes.Include(t => t.DIADIEM);
            return View(tOADOes.ToList());
        }

        // GET: TOADOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOADO tOADO = db.TOADOes.Find(id);
            if (tOADO == null)
            {
                return HttpNotFound();
            }
            return View(tOADO);
        }

        // GET: TOADOes/Create
        public ActionResult Create(int id, string idkh)
        {
            //ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "DIADIEM_TEN");
            //return View();
            TempData["IDKH"] = idkh;
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                TempData["ID"] = id;
                return View();
            }
        }

        // POST: TOADOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TOADO_ID,TOADO_LAT,TOADO_LNG,DIADIEM_ID")] TOADO tOADO, int id, string idkh)
        {
            if (ModelState.IsValid)
            {
                db.TOADOes.Add(tOADO);
                db.SaveChanges();
                return RedirectToAction("/KH_DiaDiemCT/", "KH_DiaDiem", new { id = id, idkh = idkh, index = 7 });
            }

            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "KHACHHANG_ID", tOADO.DIADIEM_ID);
            return View(tOADO);
        }

        // GET: TOADOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOADO tOADO = db.TOADOes.Find(id);
            if (tOADO == null)
            {
                return HttpNotFound();
            }
            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "KHACHHANG_ID", tOADO.DIADIEM_ID);
            return View(tOADO);
        }

        // POST: TOADOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TOADO_ID,TOADO_LAT,TOADO_LNG,DIADIEM_ID")] TOADO tOADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tOADO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "KHACHHANG_ID", tOADO.DIADIEM_ID);
            return View(tOADO);
        }

        // GET: TOADOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOADO tOADO = db.TOADOes.Find(id);
            if (tOADO == null)
            {
                return HttpNotFound();
            }
            return View(tOADO);
        }

        // POST: TOADOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TOADO tOADO = db.TOADOes.Find(id);
            db.TOADOes.Remove(tOADO);
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

        public ActionResult XemBanDo(int dd, string idkh)
        {
            TempData["ID"] = dd;
            TempData["IDKH"] = idkh;
            var bando = (from a in db.TOADOes where a.DIADIEM_ID == dd select a).OrderByDescending(a => a.TOADO_ID).FirstOrDefault();
            if(bando == null)
            {
                return RedirectToAction("Create", new { id = dd, idkh = idkh });
            }
            return View(bando);
        }

        public ActionResult KH_BanDo(int dd)
        {
            TempData["ID"] = dd;
            var bando = (from a in db.TOADOes where a.DIADIEM_ID == dd select a).OrderByDescending(a => a.TOADO_ID).FirstOrDefault();
            return View(bando);
        }
    }
}
