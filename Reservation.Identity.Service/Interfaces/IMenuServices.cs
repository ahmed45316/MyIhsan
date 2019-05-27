using Reservation.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Identity.Service.Interfaces
{
    public interface IMenuServices
    {
        Task<IResponseResult> GetMenu(string userId);
    }
}
