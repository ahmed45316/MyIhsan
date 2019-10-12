using Basic.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Service.UnitOfWork
{
    public interface IIdentityUnitOfWork<T> : IDisposable where T : class
    {
        DbContext IdentityDbContext { get; set; }
        IRepository<T> Repository { get; }
        Task<int> SaveChanges();
        void StartTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
