
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyIhsan.API.Controllers.Base;
using MyIhsan.Common.Core;
using MyIhsan.Common.Parameters;
using MyIhsan.Identity.Service.Core;
using MyIhsan.Identity.Service.Dtos;
using MyIhsan.Identity.Service.Interfaces;
using System.Threading.Tasks;

namespace MyIhsan.API.Controllers.Secuirty
{
    /// <inheritdoc />
    public class RoleController : BaseMainController
    {
        private readonly IRoleServices _roleServices;
        /// <inheritdoc />
        public RoleController(IHandlerResponse responseHandler, ITokenBusiness tokenBusiness, IRoleServices roleServices) : base(responseHandler, tokenBusiness)
        {
            _roleServices = roleServices;
        }
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IDataPagging> GetAll(GetAllRoleParameters parameters)
        {
            var repositoryResult = await _roleServices.GetRoles(parameters);
            return repositoryResult;
        }
        /// <summary>
        /// Get  Role by RoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("{roleId}")]
        public async Task<IResult> Get(string roleId)
        {
            var repositoryResult = await _roleServices.GetByIdAsync(roleId);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Add new Role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Add(RoleDto model)
        {
            var repositoryResult = await _roleServices.AddAsync(model);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Update Role
        /// </summary>
        ///<param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IResult> Update(RoleDto model)
        {
            var repositoryResult = await _roleServices.UpdateAsync(model);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Remove Role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IResult> Remove(string id)
        {
            var repositoryResult = await _roleServices.DeleteAsync(id);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
       
        /// <summary>
        /// check Name is Exists
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{name}/{id?}")]
        public async Task<IResult> IsNameExists(string name, string id = null)
        {
            var repositoryResult = await _roleServices.IsNameExists(name,id);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        
    }
}