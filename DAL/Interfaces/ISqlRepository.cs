using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Entities.Repositories;

namespace DAL.Interfaces
{
    public interface ISqlRepository : IDisposable
    {
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;
        TEntity GetById<TEntity>(int id) where TEntity : BaseModel<int>;
        TEntity GetById<TEntity>(string id) where TEntity : BaseModel<string>;
        void Insert<TEntity>(TEntity entity) where TEntity : class;
        void SaveChanges();
    }
    public enum Status { Succees=1,Failure,Dublication }
}
