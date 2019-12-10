using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDD.Areas.Admin.Controllers
{
    
    public class ChartController : BaseController
    {
        // GET: Admin/Chart
        public ActionResult Index()
        {
            return View();
        }
    }
}