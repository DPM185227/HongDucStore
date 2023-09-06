using System.Web.Mvc;

namespace HongDucStore.Areas.Admin
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
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "LoginAdmin", Controller="Home", id = UrlParameter.Optional },
                new[] {"HongDucStore.Areas.Admin.Controllers"}
            );
        }
    }
}