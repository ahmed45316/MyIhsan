﻿using Microsoft.EntityFrameworkCore.Query;
using MyIhsan.Common.Extensions;
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
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        Task<IEnumerable<T>> GetAllAsync(IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate = null, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        Task<(int, IEnumerable<T>)> FindPaggedAsync(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = 0, IEnumerable<SortModel> orderByCriteria = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true);
        T Add(T newEntity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity, object key);
        void Update(T originalEntity, T newEntity);
        void UpdateRange(IEnumerable<T> newEntitie);
        void Remove(T entity);
        void Remove(Expression<Func<T, bool>> predicate);
        void RemoveRange(IEnumerable<T> entities);
        Task<bool> IsExists(Expression<Func<T, bool>> predicate);
    }
}
