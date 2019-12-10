using System.Web.Mvc;

namespace QLDD.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
            //"Admin_default",
            //"Admin/{controller}/{action}/{id}",
            //new { action = "Index",cn, id = UrlParameter.Optional },
            //new[] { "QLDD.Areas.Admin.Controllers" }
            "Admin_default",
            "Admin/{controller}/{action}/{id}",
            new { action = "Index", controller = "Home", id = UrlParameter.Optional },
            namespaces: new string[] { "QLDD.Areas.Admin.Controllers" }
            );
        }
    }
}