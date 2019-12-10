using QLDD.Code;
using QLDD.Common;
using QLDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BotDetect.Web.Mvc;

namespace QLDD.Controllers
{
    public class LoginController : Controller
    {
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
                var dao = new UserDao();
                var result = dao.Login(model.UserName, model.Password);
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.KHACHHANG_ID;

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

            //if (Membership.ValidateUser(model.UserName, model.Password) && ModelState.IsValid)
            //{

            //    FormsAuthentication.SetAuthCookie(model.UserName, model.Rememberme);
            //    return RedirectToAction("DanhSachDiaDiem", "DiaDiem");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
            //}
            //return View(model);
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidationActionFilter("CaptchaCode", "Register", "Mã xác thực không đúng!")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckKH(model.TK))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã được sử dụng");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã được sử dụng");
                }
                else
                {
                    var user = new KHACHHANG();
                    user.KHACHHANG_ID = model.TK;
                    user.KHACHHANG_TEN = model.TenKH;
                    user.KHACHHANG_PASSWORD = model.MK;
                    user.KHACHHANG_SDT = model.SDT;
                    user.KHACHHANG_EMAIL = model.Email;
                    var result = dao.Insert(user);
                    if (result != null)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                       

                    }
                    else
                    {
                        ModelState.AddModelError("","Đăng ký không thành công");
                    }

                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
            
        }
    }
}