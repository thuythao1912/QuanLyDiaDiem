using QLDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDD.Controllers
{
    public class HinhSanPhamController : Controller
    {
        public QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();
        // GET: HinhSanPham
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult HinhSanPham(int? id)
        {
            var hsp = from item in db.HINHSANPHAMs where item.SANPHAM_ID == id select item;
            return View(hsp);
        }
    }

     
}