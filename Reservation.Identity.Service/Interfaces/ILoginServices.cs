using MyIhsan.Common.Core;
using MyIhsan.Common.Parameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Identity.Service.Interfaces
{
    public interface ILoginServices
    {
        Task<IResponseResult> Login(LoginParameters parameters);
    }
}
