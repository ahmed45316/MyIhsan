
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservation.API.Controllers.Base;
using Reservation.Common.Core;
using Reservation.Common.IdentityInterfaces;
using Reservation.Common.Parameters;
using Reservation.Identity.Service.Core;
using Reservation.Identity.Service.Dtos;
using Reservation.Identity.Service.Interfaces;
using System.Threading.Tasks;

namespace Reservation.API.Controllers.Secuirty
{
    /// <inheritdoc />
    [Route("[controller]")]
    [ApiController]
    [Authorize]
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
        [HttpPost("GetAll")]
        public async Task<IDataPagging> GetRoles(GetAllRoleParameters parameters)
        {
            var repositoryResult = await _roleServices.GetRoles(parameters);
            return repositoryResult;
        }
        /// <summary>
        /// Get  Role by RoleId
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("Get/{roleId}")]
        public async Task<IResult> GetRole(string roleId)
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
        [HttpPost("Add")]
        public async Task<IResult> AddRole(GetRoleDto model)
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
        [HttpPut("Update")]
        public async Task<IResult> UpdateRole(UpdateRoleDto model)
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
        [HttpDelete("RemoveById/{id}")]
        public async Task<IResult> RemoveRoleById(string id)
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
        [HttpGet(nameof(GetRoleByName)+"/{roleName?}")]
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
        [Route("IsNameExists/{name}/{id?}")]
        [HttpGet]
        public async Task<IResult> IsNameExists(string name, string id = null)
        {
            var repositoryResult = await _roleServices.IsNameExists(name,id);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        
    }
}