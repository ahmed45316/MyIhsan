using AutoMapper;
using Reservation.Common.Core;
using Reservation.Identity.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Identity.Service.Core
{
   
    public class BaseService<T> where T : class
    {
        protected readonly IIdentityUnitOfWork<T> _unitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IResponseResult ResponseResult;
        protected internal BaseService(IBusinessBaseParameter<T> businessBaseParameter)
        {
            _unitOfWork = businessBaseParameter.UnitOfWork;
            ResponseResult = businessBaseParameter.ResponseResult;
            Mapper = businessBaseParameter.Mapper;
        }
    }
}
