using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Infrastructure.Repository
{ 
    public class Repository<T> : IRepository<T> where T : class
    {
        private const bool TrueExpression = true;
        protected readonly DbContext Context;
        protected DbSet<T> DbSet;
        public Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }
        public async Task<T> GetAsync(params object[] keys)
        {
            return await DbSet.FindAsync(keys);
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate,Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.FirstOrDefaultAsync();
            
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = DbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }
            return await query.ToListAsync();
        }
        public T Add(T newEntity)
        {
            return DbSet.Add(newEntity).Entity;
        }
        public void AddRange(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
        }
        public void Update(T entity)
        {
            var key = GetKeyValue(entity);

            Update(entity, key);
        }
        public void Update(T entity, object key)
        {
            var originalEntity = DbSet.Find(key);

            Update(originalEntity, entity);
        }
        public void Update(T originalEntity, T newEntity)
        {
            Context.Entry(originalEntity).CurrentValues.SetValues(newEntity);
        }
        public void UpdateRange(IEnumerable<T> newEntitie)
        {
            Context.UpdateRange(newEntitie);
        }
        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
        public void Remove(Expression<Func<T, bool>> predicate)
        {
            var objects = FindAsync(predicate);
            foreach (var obj in objects.Result)
            {
                DbSet.Remove(obj);
            }
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
        public string GetKeyField(Type type)
        {
            var allProperties = type.GetProperties();

            var keyProperty = allProperties.SingleOrDefault(p => p.IsDefined(typeof(KeyAttribute)));

            return keyProperty?.Name;
        }
        public int GetNextKeySequence()
        {
            var query = DbSet.OfType<T>();
            var theLast = query.LastOrDefault();
            if (theLast == null) return 1;
            var key = theLast.GetType().GetProperties().FirstOrDefault(
                    p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);
            if (key != null)
            {
                var keyValue = key.GetValue(theLast, null).ToString();
                int.TryParse(keyValue, out int valueResult);
                return valueResult;
            }

            return 0;
        }
        public object GetKeyValue(T t)
        {
            var key =
                typeof(T).GetProperties().FirstOrDefault(
                    p => p.GetCustomAttributes(typeof(KeyAttribute), true).Length != 0);
            return key?.GetValue(t, null);
        }
        public async Task<bool> Contains(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
        }
        
    }
}
