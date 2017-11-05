using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentAccountingSystem.Startup))]
namespace StudentAccountingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
