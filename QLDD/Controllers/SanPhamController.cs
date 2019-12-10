using QLDD.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDD.Models;
using System.Net;
using System.Data.Entity;

namespace QLDD.Controllers
{
 
    public class SanPhamController : BaseController
    {
        public QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();
        // GET: SanPham

        public ActionResult DSSanPham(int? id, string idkh)
        {

            if (id == 0 || idkh == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                TempData["IDKH"] = idkh;
                TempData["ID"] = id;

                var a = from b in db.SANPHAMs where b.DIADIEM_ID == id select b;
                return View(a);
            }
        }

        public ActionResult CreateSP(int id, string idkh)
        {
            TempData["IDKH"] = idkh;
            TempData["ID"] = id;
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

        [HttpPost, ActionName("CreateSP")]
        [ValidateInput(false)]
        public ActionResult CreateSP(SANPHAM SP, int id, string idkh)
        {
            try
            {
                SP.DIADIEM_ID = id;
                db.SANPHAMs.Add(SP);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Lỗi không thể lưu");
            }
            return RedirectToAction("/KH_DiaDiemCT/", "KH_DiaDiem", new { id = id, idkh = idkh, index = 6 });
        }


        [HttpGet]
        public ActionResult Editsp(int id, int iddd, string idkh)
        {
            TempData["IDKH"] = idkh;
            TempData["IDDD"] = iddd;
            if (id == 0)
            {
                return HttpNotFound();
            }
            else
            {


                SANPHAM SP = db.SANPHAMs.SingleOrDefault(s => s.SANPHAM_ID == id);
                return View(SP);
            }
        }

        [HttpPost, ActionName("EditSP")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditSP(int id, int iddd, string idkh)
        {
            
            var spUpdate = db.SANPHAMs.Find(id);
            if (TryUpdateModel(spUpdate, "", new string[] { "SANPHAM_TEN","SANPHAM_GIA", "SANPHAM_MOTA" }))
            {
                try
                {
                    db.Entry(spUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Lỗi khi lưu");
                }
            }

            return RedirectToAction("/KH_DiaDiemCT/", "KH_DiaDiem", new { id = iddd, idkh = idkh, index = 6 });
        }


        [HttpGet]
        public ActionResult DeleteSP(int id, int iddd, string idkh)
        {
            var ha = from b in db.HINHSANPHAMs where b.SANPHAM_ID == id select b;
            foreach (HINHSANPHAM a in ha)
            {

                if (ha == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.HINHSANPHAMs.Remove(a);
            }
            SANPHAM SP = db.SANPHAMs.SingleOrDefault(s => s.SANPHAM_ID == id && s.DIADIEM_ID == iddd);

            db.SANPHAMs.Remove(SP);
            if (SP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {

                db.SaveChanges();

            }
            return RedirectToAction("/DSSanPham/" + iddd, new { idkh = idkh });

        }

        public ActionResult DSHSP(int id, int iddd, string idkh)
        {
            TempData["ID"] = id;
            TempData["IDDD"] = iddd;
            TempData["IDKH"] = idkh;

            if (id == 0)
            {
                return HttpNotFound();
            }
            else
            {
                var a = (from b in db.HINHSANPHAMs where b.SANPHAM_ID == id select b);
                return View(a);
            }
        }

        [HttpGet]
        public ActionResult DeleteHSP(int id, string idkh, int iddd)
        {
            HINHSANPHAM HSP = db.HINHSANPHAMs.SingleOrDefault(s => s.HSP_ID == id);

            db.HINHSANPHAMs.Remove(HSP);
            if (HSP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {

                db.SaveChanges();

            }
            return RedirectToAction("/KH_DiaDiemCT/", "KH_DiaDiem", new { id = iddd, idkh = idkh, index = 6 });

        }
        public ActionResult ThemHinhSP(int id, string idkh)
        {
            TempData["IDKH"] = idkh;
            TempData["IDSP"] = id;
            return View();
        }

        [HttpPost, ActionName("ThemHinhSP")]
        [ValidateInput(false)]
        public ActionResult ThemHinhSP(HINHSANPHAM hinhsp, int id,int iddd,string idkh)
        {
            try
            {
                var check = new Check();
                var a = check.CheckHinhSP(id, hinhsp.HSP_LINK.ToString());
                if (a == true)

                {
                    TempData["IDSP"] = id;
                    //ModelState.AddModelError("", "Sản phẩm đã tồn tại hình ảnh này");
                    return RedirectToAction("/KH_DiaDiemCT/", "KH_DiaDiem", new { id = iddd, idkh = idkh, index = 5 });
                }
                else
                {
                    TempData["IDSP"] = id;
                    TempData["IDKH"] = idkh;
                    db.HINHSANPHAMs.Add(hinhsp);
                    db.SaveChanges();
                }
                return RedirectToAction("/KH_DiaDiemCT/", "KH_DiaDiem", new { id=iddd, idkh = idkh, index = 6 });
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            return RedirectToAction("/KH_DiaDiemCT/", "KH_DiaDiem", new { id = iddd, idkh = idkh, index = 6 });

        }


       
    }
}