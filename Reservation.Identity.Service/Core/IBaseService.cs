using Reservation.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Identity.Service.Core
{
    public interface IBaseService<T, TDto>
    {
        Task<IResponseResult> GetAllAsync();
        Task<IResponseResult> AddAsync(TDto model);
     }
}
