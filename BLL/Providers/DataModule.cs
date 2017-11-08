using Autofac;
using BLL.Identity;
using DAL.Entities;
using DAL.Entities.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using StudentAccountingSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.Entities.Repositories;
using DAL.Interfaces;

namespace BLL.Providers
{
    public class DataModule : Module
    {
        private string _connStr;
        private IAppBuilder _app;

        public DataModule(string connString, IAppBuilder app)
        {
            _connStr = connString;
            _app = app;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AppDBContext(this._connStr)).As<IAppDBContext>().InstancePerRequest();
            builder.Register(ctx =>
            {
                var context = (AppDBContext)ctx.Resolve<IAppDBContext>();
                return context;
            }).AsSelf().InstancePerDependency();
            builder.RegisterType<Service.ApplicationUserStore>().As<IUserStore<AppUser>>().InstancePerRequest();
            builder.RegisterType<Service.ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<Service.ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => _app.GetDataProtectionProvider()).InstancePerRequest();
            builder.RegisterType<AccountProvider>().As<IAccountProvider>().InstancePerRequest();

            builder.RegisterType<SqlRepository>().As<ISqlRepository>().InstancePerRequest();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerRequest();

            base.Load(builder);
        }
    }
}
