using AutoMapper;
using MyIhsan.Common.Core;
using MyIhsan.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Service.Core
{
    public interface IServiceBaseParameter<T> where T : class
    {
        IMapper Mapper { get; set; }
        IIdentityUnitOfWork<T> UnitOfWork { get; set; }
        IResponseResult ResponseResult { get; set; }
    }
}
