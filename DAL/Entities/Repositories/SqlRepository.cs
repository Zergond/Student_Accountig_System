using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Entities.Repositories
{
    public class SqlRepository : ISqlRepository
    {
        private readonly IAppDBContext _context;
        public SqlRepository(IAppDBContext context)
        {
            _context = context;
        }
        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            GetEntities<TEntity>().Remove(entity);
        }
        private IDbSet<TEntity> GetEntities<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return GetEntities<TEntity>();
        }

        public TEntity GetById<TEntity>(int id) where TEntity : BaseModel<int>
        {
            return GetAll<TEntity>()
                .SingleOrDefault(e => e.Id == id);
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            GetEntities<TEntity>().Add(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public TEntity GetById<TEntity>(string id) where TEntity : BaseModel<string>
        {
            return GetAll<TEntity>()
                .SingleOrDefault(e => e.Id == id);
        }
    }
    public abstract class BaseModel<T>
    {
        [Key]
        public virtual T Id { get; set; }
    }
}
