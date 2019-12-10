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
using PagedList;
using PagedList.Mvc;

namespace QLDD.Controllers
{
  
    public class DiaDiemController : BaseController
    {
        public QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();
        // GET: DiaDiem
        public ActionResult DSDiaDiem(string id, int? page)
        {
            TempData["ID"] = id;
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                var a = (from b in db.DIADIEMs where b.KHACHHANG_ID == id select b).OrderBy(x => x.DIADIEM_ID).ToPagedList(page ?? 1, 4);
                return View(a);
            }
        }


        public ActionResult DSDiaDiemCT(int id, string idkh)
        {
            TempData["rs"] = 0;
            TempData["ID"] = id;
            TempData["IDKH"] = idkh;
            if (id == 0)
            {
                return HttpNotFound();
            }
            else
            {
                var a = from b in db.DIADIEMs where b.DIADIEM_ID == id select b;
                return View(a);
            }
        }


        public ActionResult CreateDD(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                TempData["ID"] = id;
                DIADIEM d = new DIADIEM();
                List<LOAI> l = (from i in db.LOAIs select i).ToList();
                ViewBag.Loai = new SelectList(l, "LOAI_ID", "LOAI_TEN", d.LOAI_ID);
                return View();
            }
        }

        [HttpPost, ActionName("CreateDD")]
        [ValidateInput(false)]
        public ActionResult CreateDD(DIADIEM DD, string id)
        {

            try
            {
                DD.KHACHHANG_ID = id;
                db.DIADIEMs.Add(DD);
                db.SaveChanges();


            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Lỗi không thể lưu");
            }
            return RedirectToAction("/DSDiaDiem/" + id);
        }

        [HttpGet]
        public ActionResult Editdd(int id, string idkh)
        {
            TempData["ID"] = id;
            TempData["IDKH"] = idkh;
            if (id == 0)
            {
                return HttpNotFound();
            }
            else
            {
                DIADIEM d = new DIADIEM();
                List<LOAI> l = (from i in db.LOAIs select i).ToList();
                ViewBag.Loai = new SelectList(l, "LOAI_ID", "LOAI_TEN", d.LOAI_ID);
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                DIADIEM DD = db.DIADIEMs.SingleOrDefault(s => s.DIADIEM_ID == id);
                return View(DD);
            }
        }

        [HttpPost, ActionName("EditDD")]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditDD(int id, string idkh)
        {
            TempData["ID"] = id;
            TempData["IDKH"] = idkh;
            var ddUpdate = db.DIADIEMs.Find(id);
            if (TryUpdateModel(ddUpdate, "", new string[] { "LOAI_ID", "DIADIEM_TEN", "DIADIEM_DIACHI", "DIADIEM_WEB", "DIADIEM_MOTA" }))
            {
                try
                {
                    db.Entry(ddUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Lỗi khi lưu");
                }
            }

            return RedirectToAction("/KH_DiaDiemCT/" + id, "KH_DiaDiem", new { idkh = idkh });
        }



        [HttpGet]
        public ActionResult DeleteDD(int id, string idkh)
        {

            var ha = from b in db.HINHDIADIEMs where b.DIADIEM_ID == id select b;
            var bd = from c in db.BAIDANGs where c.DIADIEM_ID == id select c;
            var td = from t in db.TOADOes where t.DIADIEM_ID == id select t;
            foreach (HINHDIADIEM a in ha)
            {

                if (ha == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.HINHDIADIEMs.Remove(a);
            }

            foreach (BAIDANG d in bd)
            {
                if (bd == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }

                db.BAIDANGs.Remove(d);
            }

            foreach (TOADO e in td)
            {

                if (td == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.TOADOes.Remove(e);
            }
            db.SaveChanges();

            DIADIEM DD = db.DIADIEMs.SingleOrDefault(s => s.DIADIEM_ID == id && s.KHACHHANG_ID.ToString() == idkh);

            db.DIADIEMs.Remove(DD);
            db.SaveChanges();


            return RedirectToAction("DSDiaDiem/" + idkh);

        }


        public ActionResult DSHADD(int id, string idkh)
        {
            TempData["ID"] = id;    
            TempData["IDKH"] = idkh;

            if (id == 0)
            {
                return HttpNotFound();
            }
            else
            {
                var a = (from b in db.HINHDIADIEMs where b.DIADIEM_ID == id select b);
                return View(a);
            }
        }

        [HttpGet]
        public ActionResult DeleteHA(int id, string idkh, int iddd)
        {

            HINHDIADIEM HA = db.HINHDIADIEMs.SingleOrDefault(s => s.HDD_ID == id);

            db.HINHDIADIEMs.Remove(HA);
            if (HA == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {

                db.SaveChanges();

            }
            return RedirectToAction("/KH_DiaDiemCT/" + iddd, "KH_DiaDiem", new { idkh = idkh, index = 1 });

        }


        public ActionResult ThemHinhDiaDiem(int id)
        {
            TempData["ID"] = id;
            return View();
        }

        [HttpPost, ActionName("ThemHinhDiaDiem")]
        [ValidateInput(false)]
        public ActionResult ThemHinhDiaDiem(HINHDIADIEM hinhdiadiem, int id, string idkh)
        {

            TempData["IDKH"] = idkh;
            if (id == 0)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    var check = new Check();
                    var a = check.CheckHinhDD(id, hinhdiadiem.HDD_LINK);
                    if (a == true)

                    {
                        TempData["ID"] = id;
                        return RedirectToAction("/KH_DiaDiemCT/" + id, "KH_DiaDiem", new { idkh = idkh , index = 3});
                    }
                    else
                    {
                        TempData["ID"] = id;
                        TempData["IDKH"] = idkh;
                        db.HINHDIADIEMs.Add(hinhdiadiem);
                        db.SaveChanges();
                    }
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Error Save Data");
                }
                
                return RedirectToAction("/KH_DiaDiemCT/" + id, "KH_DiaDiem", new { idkh = idkh , index = 1});
            }
        }
        //[HttpGet]
        //public ActionResult ChiTietDiaDiem(int? id)
        //{
        //    if (id == 0)
        //    {
        //        return HttpNotFound();
        //    }
        //    var view = db.DIADIEMs.SingleOrDefault(item => item.DIADIEM_ID == id);
        //    if (view == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(view);
        //}

        //public ActionResult DanhSachDiaDiem(int ?id, int ?page)
        //{
        //    //TempData["ID"] = id;
        //    if (id == null)
        //    {
        //        var dsDiaDiem = (from b in db.DIADIEMs select b).OrderBy(x => x.DIADIEM_ID).ToPagedList(page ??1, 4);
        //        //var dsDiaDiem = (from b in db.DIADIEMs select b);
        //        return View(dsDiaDiem);
        //    }
        //    else
        //    {
        //        var dsDiaDiem = (from b in db.DIADIEMs where b.LOAI_ID == id select b).OrderBy(x => x.DIADIEM_ID).ToPagedList(page ??1, 4);
        //        //var dsDiaDiem = (from b in db.DIADIEMs where b.LOAI_ID == id select b);
        //        return View(dsDiaDiem);
        //    }
        //}
        //[HttpGet]
        //public ActionResult SearchDanhSachDiaDiem(string searchString)
        //{
        //    if (searchString == null)
        //    {
        //        var dsDiaDiem = (from b in db.DIADIEMs select b);
        //        return View(dsDiaDiem);
        //    }
        //    else
        //    {
        //        var dsDiaDiem = (from b in db.DIADIEMs select b).Where(x => x.DIADIEM_TEN.Contains(searchString)).OrderBy(x=>x.DIADIEM_TEN);
        //        return View(dsDiaDiem);
        //    }
        //}

        //[HttpGet]
        //public ActionResult SearchDiaDiemTheoTinh(string searchString)
        //{
        //    if (searchString == null)
        //    {
        //        var dsDiaDiem = (from b in db.DIADIEMs select b);
        //        return View(dsDiaDiem);
        //    }
        //    else
        //    {
        //        var dsDiaDiem = (from b in db.DIADIEMs select b).Where(x => x.DIADIEM_DIACHI.Contains(searchString)).OrderBy(x => x.DIADIEM_TEN);
        //        return View(dsDiaDiem);
        //    }
        //}

        //public ActionResult KH_DiaDiemCT(int id, string idkh, int index = 0, int idsp=0)
        //{
        //    TempData["ID"] = id;
        //    TempData["IDKH"] = idkh;
        //    TempData["Index"] = index;
        //    TempData["IDSP"] = idsp;
        //    return View();

        //}

        

    }
}