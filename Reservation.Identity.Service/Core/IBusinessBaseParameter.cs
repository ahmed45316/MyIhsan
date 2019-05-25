using AutoMapper;
using Reservation.Common.Core;
using Reservation.Identity.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Identity.Service.Core
{
    public interface IBusinessBaseParameter<T> where T : class
    {
        IMapper Mapper { get; set; }
        IIdentityUnitOfWork<T> UnitOfWork { get; set; }
        IResponseResult ResponseResult { get; set; }
    }
}
