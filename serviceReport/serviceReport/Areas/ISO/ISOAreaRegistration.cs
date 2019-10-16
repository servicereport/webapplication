using System.Web.Mvc;

namespace serviceReport.Areas.ISO
{
    public class ISOAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ISO";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ISO_default",
                "ISO/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}