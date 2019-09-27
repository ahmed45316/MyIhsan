using AutoMapper;
using MyIhsan.Common.Core;
using MyIhsan.Identity.Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyIhsan.Identity.Service.Core
{
    public interface IBusinessBaseParameter<T> where T : class
    {
        IMapper Mapper { get; set; }
        IIdentityUnitOfWork<T> UnitOfWork { get; set; }
        IResponseResult ResponseResult { get; set; }
    }
}
