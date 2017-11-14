using DAL.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAppDBContext : IDisposable
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet<Student>Students { get; set; }
        int SaveChanges();
    }

}
