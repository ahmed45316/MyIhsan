using Basic.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using MyIhsan.Identity.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Identity.Service.UnitOfWork
{
    public class IdentityUnitOfWork<T> : IIdentityUnitOfWork<T> where T : class
    {
        private IDbContextTransaction _transaction;

        public IRepository<T> Repository { get; }
        public DbContext IdentityDbContext { get; set; }

        public IdentityUnitOfWork(IConfiguration config )//, DbContext context)
        {
            var connection = config.GetConnectionString("IdentityContext");
            var optionsBuilder = new DbContextOptionsBuilder<ModelContext>();
            optionsBuilder.UseOracle(connection);
            IdentityDbContext = new ModelContext(optionsBuilder.Options);
            Repository = new Repository<T>(IdentityDbContext);
        }

        public async Task<int> SaveChanges()
        {
            return await IdentityDbContext.SaveChangesAsync();
        }

        public void StartTransaction()
        {
            _transaction = IdentityDbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
            _transaction.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (IdentityDbContext == null)
            {
                return;
            }

            IdentityDbContext.Dispose();
            IdentityDbContext = null;
        }
    }
}
