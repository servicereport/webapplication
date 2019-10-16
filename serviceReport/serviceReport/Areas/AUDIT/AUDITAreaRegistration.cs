using System.Web.Mvc;

namespace serviceReport.Areas.AUDIT
{
    public class AUDITAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AUDIT";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AUDIT_default",
                "AUDIT/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}