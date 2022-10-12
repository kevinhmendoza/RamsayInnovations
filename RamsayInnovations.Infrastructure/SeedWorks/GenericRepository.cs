using Microsoft.EntityFrameworkCore;
using RamsayInnovations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RamsayInnovations.Infrastructure.SeedWorks
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
       where T : BaseEntity
    {
        protected readonly Sample_DBContext _db;
        protected readonly DbSet<T> _dbset;


        protected GenericRepository(Sample_DBContext context)
        {
            _db = context;
            _dbset = context.Set<T>();
        }

        public virtual Task<List<T>> GetAllAsync()
        {

            return _dbset.AsNoTracking().ToListAsync<T>();
        }

        public virtual ValueTask<T> FindAsync(object id)
        {
            return _dbset.FindAsync(id);
        }


        public virtual T Add(T entity)
        {
            return _dbset.Add(entity).Entity;
        }

        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }
        public virtual void Edit(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            bool query = _dbset.Any(predicate);
            return query;
        }
    }
}
