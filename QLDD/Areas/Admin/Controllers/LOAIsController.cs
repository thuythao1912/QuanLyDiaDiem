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
    
    public class LOAIsController : BaseController
    {
        private QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();

        // GET: Admin/LOAIs
        public ActionResult Index(int ? page)
        {
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //List<LOAI> listLoai;
            var listLoai = (from l in db.LOAIs select l).ToList();
            return View(listLoai.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/LOAIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAI lOAI = db.LOAIs.Find(id);
            if (lOAI == null)
            {
                return HttpNotFound();
            }
            return View(lOAI);
        }

        // GET: Admin/LOAIs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LOAIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LOAI_ID,LOAI_TEN")] LOAI lOAI)
        {
            if (ModelState.IsValid)
            {
                db.LOAIs.Add(lOAI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOAI);
        }

        // GET: Admin/LOAIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAI lOAI = db.LOAIs.Find(id);
            if (lOAI == null)
            {
                return HttpNotFound();
            }
            return View(lOAI);
        }

        // POST: Admin/LOAIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LOAI_ID,LOAI_TEN")] LOAI lOAI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOAI);
        }

        // GET: Admin/LOAIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAI lOAI = db.LOAIs.Find(id);
            if (lOAI == null)
            {
                return HttpNotFound();
            }
            return View(lOAI);
        }

        // POST: Admin/LOAIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                LOAI lOAI = db.LOAIs.Find(id);
                db.LOAIs.Remove(lOAI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
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
