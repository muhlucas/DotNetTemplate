using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

namespace DotNetTemplate.MVC.App_Start
{
    public class AutofacConfiguration
    {
        public static void Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = Application.Configuration.AutofacConfiguration.Builder(builder);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}