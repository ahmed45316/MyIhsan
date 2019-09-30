using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Infrastructure.Repository
{ 
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(params object[] keys);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        Task<bool> Contains(Expression<Func<T, bool>> predicate);
        T Add(T newEntity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Update(T entity, object key);
        void Update(T originalEntity, T newEntity);
        void UpdateRange(IEnumerable<T> newEntitie);
        void Remove(T entity);
        void Remove(Expression<Func<T, bool>> predicate);
        void RemoveRange(IEnumerable<T> entities);
        object GetKeyValue(T t);
        string GetKeyField(Type type);
        int GetNextKeySequence();
    }
}
