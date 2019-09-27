
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
            var repositoryResult = await _roleServices.GetRole(roleId);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Add new Role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Add(GetRoleDto model)
        {
            var repositoryResult = await _roleServices.AddRole(model);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Update Role
        /// </summary>
        ///<param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IResult> Update(UpdateRoleDto model)
        {
            var repositoryResult = await _roleServices.UpdateRole(model);
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
            var repositoryResult = await _roleServices.RemoveRoleById(id);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Get  Role by Name from stored
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet("{roleName?}")]
        public IActionResult GetRoleByName(string roleName=null)
        {
            var repositoryResult = _roleServices.GetRoleFromStored(roleName);
            return Ok(repositoryResult);
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