using QLDD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDD.Areas.Admin.Models;
using QLDD.Code;

namespace QLDD.Controllers
{
    public class HomeController : Controller
    {
        public QUANLYDIADIEMEntities db = new QUANLYDIADIEMEntities();

        public ActionResult Index(int? id)
        {

            string Chuoi = "";
            string Chuoi1 = "";

            List<DIADIEM> diadiem = new List<DIADIEM>();
            //var diadiem = (from p in db.DIADIEMs orderby p.DIADIEM_ID ascending select p).Take(9).ToList();
            if ((id == null) || (id == 0))
            {
                diadiem = (from p in db.DIADIEMs orderby p.DIADIEM_ID ascending select p).Take(9).ToList();
            }
            else
            {
                diadiem = (from p in db.DIADIEMs where p.LOAI_ID == id select p).Take(9).ToList();
            }

            //Chuoi1 += "<p>" + diadiem.GetType() + "</p>";

            for (int i = 0; i < diadiem.Count; i++)
            {
                var hinhanh = (from h in diadiem[i].HINHDIADIEMs orderby h.DIADIEM_ID descending select h).Take(1).ToList();
                Chuoi1 += "<div id = 'slidingDiv" + i + "' class='toggleDiv row-fluid single-project'>";
                Chuoi1 += "<div class=\"span6\">";
                if (hinhanh.Count > 0)
                {
                    Chuoi1 += "<div class='thumbnail-diadiem-mask' style='background-image: url(\"" + hinhanh[0].HDD_LINK + "\")' > </div>";
                }
                else
                {
                    Chuoi1 += "<div class='thumbnail-diadiem-mask' style='background-image: url(\"" + @"/Assets/images/404_mask.png" + "\")' > </div>";
                }
                Chuoi1 += "</div>";
                Chuoi1 += "<div class=\"span6\">";
                Chuoi1 += "<div class=\"project-description\">";
                Chuoi1 += "<div class=\"project-title clearfix\">";
                Chuoi1 += "<a href='https://localhost:44340/KH_DiaDiem/ChitietDiaDiem/" + diadiem[i].DIADIEM_ID + "'><h3>" + diadiem[i].DIADIEM_TEN + "</h3></a>";
                Chuoi1 += "<span class=\"show_hide close\">";
                Chuoi1 += "<i class=\"icon-cancel\"></i>";
                Chuoi1 += "</span>";
                Chuoi1 += "</div>";
                Chuoi1 += "<div class=\"project-info\">";
                Chuoi1 += "<div>";
                Chuoi1 += "<span>Địa chỉ: </span>" + diadiem[i].DIADIEM_DIACHI + "";
                Chuoi1 += "</div>";
                Chuoi1 += "<div>";
                Chuoi1 += "<span>Website: </span> " + diadiem[i].DIADIEM_WEB + "";
                Chuoi1 += "</div>";
                Chuoi1 += "<div>";
                Chuoi1 += "<span>Mô tả: </span>" + diadiem[i].DIADIEM_MOTA + "";
                Chuoi1 += "</div>";

                Chuoi1 += "</div>";
                Chuoi1 += "</div>";
                Chuoi1 += "</div>";
                Chuoi1 += "</div>";


                Chuoi += "<li class='span4'>";
                Chuoi += "<div class=\"thumbnail\" style='height:450px'>";
                if (hinhanh.Count > 0)
                {
                    Chuoi += "<div class='thumbnail-diadiem-mask' style='background-image: url(\"" + hinhanh[0].HDD_LINK + "\")' > </div>";
                }
                else
                {
                    Chuoi += "<div class='thumbnail-diadiem-mask' style='background-image: url(\"" + @"/Assets/images/404.png" + "\")' > </div>";
                }
                Chuoi += "<a href='#slidingDiv" + i + "' class='more show_hide' rel='#slidingDiv" + i + "'>";
                Chuoi += "<i class=\"icon-plus\"></i>";
                Chuoi += "</a>";
                Chuoi += "<a href='https://localhost:44340/KH_DiaDiem/ChitietDiaDiem/" + diadiem[i].DIADIEM_ID + "'><h3>" + diadiem[i].DIADIEM_TEN + " </h3></a>";
                Chuoi += "<h4>Địa chỉ: </h3> " + diadiem[i].DIADIEM_DIACHI + "";
                Chuoi += "<br/>";
                Chuoi += "<div class=\"mask\"> </div>";
                Chuoi += "</div>";
                Chuoi += "</li>";

            }
            ViewBag.View = Chuoi;
            ViewBag.View2 = Chuoi1;
            return View();
        }
        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListByGroupId("");
            return PartialView(model);
        }


    }
}