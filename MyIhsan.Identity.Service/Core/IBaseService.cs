using MyIhsan.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyIhsan.Identity.Service.Core
{
    public interface IBaseService<T, TDto>
    {
        Task<IResponseResult> GetAllAsync();
        Task<IResponseResult> AddAsync(TDto model);
        Task<IResponseResult> UpdateAsync(TDto model);
        Task<IResponseResult> DeleteAsync(string id);
        Task<IResponseResult> GetByIdAsync(string id);
    }
}
