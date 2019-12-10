using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLDD.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var a = Session[QLDD.Common.CommonConstants.USER_SESSION];


            if (a == null)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login/", Action = "Index" }));
            }
            base.OnActionExecuted(filterContext);
        }
    }
}