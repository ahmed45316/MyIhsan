using Reservation.Identity.Data.Context;
using Reservation.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Identity.Service.UnitOfWork
{
    public interface IIdentityUnitOfWork<T> : IDisposable where T : class
    {
        IdentityContext IdentityDbContext { get; set; }
        IRepository<T> Repository { get; }
        Task<int> SaveChanges();
        void StartTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
