using System.Web.Mvc;
using System.Web.Routing;
using ShipOfPassage.WebService;

namespace ShipOfPassage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Alexa API",
                url: "api/alexa/{id}",
                defaults: new { id = UrlParameter.Optional }
            );
        }
    }
}
