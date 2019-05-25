using AutoMapper;
using Reservation.Common.Core;
using Reservation.Identity.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Identity.Service.Core
{
    public class BusinessBaseParameter<T> : IBusinessBaseParameter<T> where T : class
    {

        public BusinessBaseParameter(IMapper mapper, IIdentityUnitOfWork<T> unitOfWork, IResponseResult responseResult)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            ResponseResult = responseResult;
        }

        public IMapper Mapper { get; set; }
        public IIdentityUnitOfWork<T> UnitOfWork { get; set; }
        public IResponseResult ResponseResult { get; set; }
    }
}
