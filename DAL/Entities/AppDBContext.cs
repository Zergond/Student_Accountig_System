using DAL.Entities.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    class AppDBContext: IdentityDbContext<AppUser>, IAppDBContext
    {
        public AppDBContext() : base("LocalConnection")
        {
            Database.SetInitializer<AppDBContext>(null);
        }

        public AppDBContext(string connString)
            : base(connString)
        {
            Database.SetInitializer<AppDBContext>(new DBInitializer());
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
