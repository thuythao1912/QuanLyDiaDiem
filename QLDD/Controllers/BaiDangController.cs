using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDD.Models;
using QLDD.Areas.Admin.Models;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Data.Entity;

namespace QLDD.Controllers
{
   
    public class BaiDangController : BaseController
    {
        public QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();
        // GET: BaiDang
        public ActionResult DSBaiDang(int? id, string idkh)
        {

            if (id == 0 || idkh == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                TempData["IDKH"] = idkh;
                TempData["ID"] = id;

                var a = from b in db.BAIDANGs where b.DIADIEM_ID == id select b;
                //var view = db.BAIDANGs.SingleOrDefault(s => s.DIADIEM_ID == id);
                return View(a);
            }
        }
        //Thêm sách mới
        public ActionResult CreateBD(int id, string idkh)
        {

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

        [HttpPost, ActionName("CreateBD")]
        [ValidateInput(false)]
        public ActionResult CreateBD(BAIDANG BD, int id, string idkh)
        {

            try
            {
                BD.DIADIEM_ID = id;
                BD.BAIDANG_DUYET = 0;
                BD.BAIDANG_NGAY = DateTime.Now;
                db.BAIDANGs.Add(BD);
                db.SaveChanges();


            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Lỗi không thể lưu");
            }
            return RedirectToAction("/KH_DiaDiemCT/", "KH_DiaDiem", new { id = id, idkh = idkh, index = 2 });
        }


        [HttpGet]
        public ActionResult Editbd(int id, int iddd, string idkh)
        {
            
            TempData["IDKH"] = idkh;
            TempData["IDDD"] = iddd;
            if (id == 0)
            {
                return HttpNotFound();
            }
            else
            {

                TempData["ID"] = id;
                BAIDANG BD = db.BAIDANGs.SingleOrDefault(s => s.BAIDANG_ID == id);
                return View(BD);
            }
        }

        [HttpPost, ActionName("EditBD")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditBD(int id, int iddd, string idkh)
        {

            var bdUpdate = db.BAIDANGs.Find(id);
            if (TryUpdateModel(bdUpdate, "", new string[] { "BAIDANG_NOIDUNG" }))
            {
                try
                {
                    db.Entry(bdUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Lỗi khi lưu");
                }
            }

            return RedirectToAction("/KH_DiaDiemCT/", "KH_DiaDiem", new { id = iddd, idkh = idkh, index = 2 });
        }

        [HttpGet]
        public ActionResult DeleteBD(int id, int iddd, string idkh)
        {

            BAIDANG BD = db.BAIDANGs.SingleOrDefault(s => s.BAIDANG_ID == id && s.DIADIEM_ID == iddd);

            db.BAIDANGs.Remove(BD);
            if (BD == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {

                db.SaveChanges();


            }
            return RedirectToAction("/DSBaiDang/" + iddd, new { idkh = idkh });

        }




    }
}