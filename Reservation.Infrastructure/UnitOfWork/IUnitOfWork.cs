using Reservation.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IRepository<T> Repository { get; }
        Task<int> SaveChanges();
        void StartTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
