using System.Web.Mvc;

namespace iTells.Areas.Marketing
{
    public class MarketingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Marketing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute("", "npp", new { area = "Marketing", controller = "NPP", action = "Index" });
            //context.MapRoute(
            //    "Marketing_default",
            //    "Marketing/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}