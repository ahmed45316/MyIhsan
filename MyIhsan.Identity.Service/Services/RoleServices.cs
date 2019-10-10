using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyIhsan.Identity.Service.Core;
using MyIhsan.Identity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyIhsan.Identity.Service.Dtos;
using MyIhsan.Common.Parameters;
using MyIhsan.Common.Core;
using MyIhsan.Identity.Entities.Entities;
using MyIhsan.Common.Extensions;
using System.Linq.Expressions;
using LinqKit;

namespace MyIhsan.Identity.Service.Services
{
    public class RoleServices : BaseService<AspNetRoles, RoleDto>, IRoleServices
    {
        public RoleServices(IServiceBaseParameter<AspNetRoles> businessBaseParameter) : base(businessBaseParameter)
        {

        }
        public async Task<IDataPagging> GetRoles(GetAllRoleParameters parameters)
        {
            //Note When data return item1 used for count and item2 for list after pagging
            int limit = parameters.PageSize;
            int offset = (parameters.PageNumber * parameters.PageSize);
            var roles = await _unitOfWork.Repository.FindPaggedAsync(predicate: PredicateBuilderFunction(parameters), skip: offset, take: limit, parameters.OrderByValue);
            var rolesPagging = roles.Item2;
            if (!rolesPagging.Any())
            {
                var result = ResponseResult.GetRepositoryActionResult(status: HttpStatusCode.NoContent, message: HttpStatusCode.NoContent.ToString());
                return new DataPagging(0, 0, 0, result);
            };
            var RolesDto = Mapper.Map<IEnumerable<AspNetRoles>, IEnumerable<RoleDto>>(rolesPagging);
            var repoResult = ResponseResult.GetRepositoryActionResult(RolesDto, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString());
            return new DataPagging(parameters.PageNumber, parameters.PageSize, roles.Item1, repoResult);
        }
        // Check IsExists
        public async Task<IResponseResult> IsNameExists(string name, string id)
        {
            var result = await _unitOfWork.Repository.IsExists(q => q.Name == name && q.Id != id && !(q.IsDeleted?? false));
            return ResponseResult.GetRepositoryActionResult(result, status: HttpStatusCode.Accepted, message: HttpStatusCode.Accepted.ToString());
        }

        static Expression<Func<AspNetRoles, bool>> PredicateBuilderFunction(GetAllRoleParameters filter)
        {
            var predicate = PredicateBuilder.New<AspNetRoles>(true);
            if (!string.IsNullOrWhiteSpace(filter.RoleName))
            {
                predicate = predicate.And(b => b.Name.ToLower().StartsWith(filter.RoleName));
            }
            return predicate;
        }
    }
}
