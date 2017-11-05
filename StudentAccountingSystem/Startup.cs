using Autofac;
using Autofac.Integration.Mvc;
using BLL.Providers;
using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(StudentAccountingSystem.Startup))]
namespace StudentAccountingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new DataModule("LocalConnection", app));

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            BLL.Startup.ConfigureAuth(app);
        }
    }
}
