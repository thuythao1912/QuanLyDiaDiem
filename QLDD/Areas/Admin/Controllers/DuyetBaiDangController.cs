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
    public class DuyetBaiDangController : Controller
    {
        // GET: Admin/DuyetBaiDang
        QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();
        public ActionResult Index(int? page)
        {
            var bAIDANGs = db.BAIDANGs.Include(b => b.DIADIEM).Include(b => b.NHANVIEN).OrderBy(b => b.BAIDANG_NGAY).ToPagedList(page ?? 1, 5);
            return View(bAIDANGs);

        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAIDANG bAIDANG = db.BAIDANGs.Find(id);
            if (bAIDANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "DIADIEM_TEN", bAIDANG.DIADIEM_ID);
            //ViewBag.NHANVIEN_ID = new SelectList(db.NHANVIENs, "NHANVIEN_ID", "NHANVIEN_TEN", bAIDANG.NHANVIEN_ID);
            return View(bAIDANG);
        }

        // POST: Admin/BAIDANGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BAIDANG_ID,NHANVIEN_ID,DIADIEM_ID,BAIDANG_NGAY,BAIDANG_DUYET,BAIDANG_NOIDUNG")] BAIDANG bAIDANG)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(bAIDANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DIADIEM_ID = new SelectList(db.DIADIEMs, "DIADIEM_ID", "KHACHHANG_ID", bAIDANG.DIADIEM_ID);
            ViewBag.NHANVIEN_ID = new SelectList(db.NHANVIENs, "NHANVIEN_ID", "NHANVIEN_TEN", bAIDANG.NHANVIEN_ID);
            return View(bAIDANG);
        }

        public ActionResult loctrangthaibd(int? trangthai)
        {
            if (trangthai == null)
            {
                var listBD = (from bd in db.BAIDANGs select bd);
                return View(listBD);
            }
            else
            {
                var listBD = (from bd in db.BAIDANGs where bd.BAIDANG_DUYET == trangthai select bd);
                return View(listBD);
            }
        }


    }
}