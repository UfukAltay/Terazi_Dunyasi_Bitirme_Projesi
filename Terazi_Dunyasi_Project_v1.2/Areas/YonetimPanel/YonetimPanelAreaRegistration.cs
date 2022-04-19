using System.Web.Mvc;

namespace Terazi_Dunyasi_Project_v12.Areas.YonetimPanel
{
    public class YonetimPanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "YonetimPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "YonetimPanel_default",
                "YonetimPanel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}