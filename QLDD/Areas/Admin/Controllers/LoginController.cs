using QLDD.Areas.Admin.Models;
using QLDD.Code;
using QLDD.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDD.Areas.Admin.Models;
using QLDD.Common;
namespace QLDD.Areas.Admin.Controllers
{ 
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AdminDao();
                var result = dao.Login(model.UserName, model.Password);
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new AdminLogin();
                    userSession.UserName = user.NHANVIEN_ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }

            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/Admin/Login");

        }
    }
}