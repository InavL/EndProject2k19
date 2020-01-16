using System.Web.Http;

namespace SlutProjekt2k19
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{action}/{id}",
                //routeTemplate: "api/{controller}/{id}",
                new
                {
                    id = RouteParameter.Optional
                }
            );
        }
    }
}