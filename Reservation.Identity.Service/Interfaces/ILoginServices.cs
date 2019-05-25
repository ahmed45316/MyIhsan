using Reservation.Common.Core;
using Reservation.Common.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Identity.Service.Interfaces
{
    public interface ILoginServices
    {
        Task<IResponseResult> Login(LoginParameters parameters);
    }
}
