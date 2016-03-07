using DotNetTemplate.MVC.App_Start;
using System.Web.Mvc;
using System.Web.Routing;

namespace DotNetTemplate.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutofacConfiguration.Start();
        }
    }
}
