using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RamsayInnovations.Domain
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<List<T>> GetAllAsync();      

        ValueTask<T> FindAsync(object id);

        bool Any(Expression<Func<T, bool>> predicate);

        T Add(T entity);

        void Delete(T entity);

        void Edit(T entity);

      
    }
}
