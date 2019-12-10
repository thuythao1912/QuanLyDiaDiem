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
    public class KH_DiaDiemController : Controller
    {
        public QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();
        // GET: KH_DiaDiem
        [HttpGet]
        public ActionResult ChiTietDiaDiem(int? id)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }
            var view = db.DIADIEMs.SingleOrDefault(item => item.DIADIEM_ID == id);
            if (view == null)
            {
                return HttpNotFound();
            }
            return View(view);
        }

        public ActionResult DanhSachDiaDiem(int? id, int? page)
        {
            //TempData["ID"] = id;
            if (id == null)
            {
                var dsDiaDiem = (from b in db.DIADIEMs select b).OrderBy(x => x.DIADIEM_ID).ToPagedList(page ?? 1, 4);
                //var dsDiaDiem = (from b in db.DIADIEMs select b);
                return View(dsDiaDiem);
            }
            else
            {
                var dsDiaDiem = (from b in db.DIADIEMs where b.LOAI_ID == id select b).OrderBy(x => x.DIADIEM_ID).ToPagedList(page ?? 1, 4);
                //var dsDiaDiem = (from b in db.DIADIEMs where b.LOAI_ID == id select b);
                return View(dsDiaDiem);
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
                var dsDiaDiem = (from b in db.DIADIEMs select b).Where(x => x.DIADIEM_TEN.Contains(searchString)).OrderBy(x => x.DIADIEM_TEN);
                return View(dsDiaDiem);
            }
        }

        [HttpGet]
        public ActionResult SearchDiaDiemTheoTinh(string searchString)
        {
            if (searchString == null)
            {
                var dsDiaDiem = (from b in db.DIADIEMs select b);
                return View(dsDiaDiem);
            }
            else
            {
                var dsDiaDiem = (from b in db.DIADIEMs select b).Where(x => x.DIADIEM_DIACHI.Contains(searchString)).OrderBy(x => x.DIADIEM_TEN);
                return View(dsDiaDiem);
            }
        }

        public ActionResult KH_DiaDiemCT(int id, string idkh, int index = 0, int idsp = 0)
        {
            TempData["ID"] = id;
            TempData["IDKH"] = idkh;
            TempData["Index"] = index;
            TempData["IDSP"] = idsp;
            return View();

        }
    }
}