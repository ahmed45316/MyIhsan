using AutoMapper;
using MyIhsan.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using MyIhsan.Common.Core;

namespace MyIhsan.Service.Core
{
    public class ServiceBaseParameter<T> : IServiceBaseParameter<T> where T : class
    {

        public ServiceBaseParameter(IMapper mapper, IIdentityUnitOfWork<T> unitOfWork, IResponseResult responseResult)
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
