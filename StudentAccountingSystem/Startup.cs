using Autofac;
using Autofac.Integration.Mvc;
using BLL.Providers;
using Hangfire;
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
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("RemoteConnection");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule(new DataModule("RemoteConnection", app));

            var container = builder.Build();
            GlobalConfiguration.Configuration.UseAutofacActivator(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            BLL.Startup.ConfigureAuth(app);
        }
    }
}
