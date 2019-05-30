using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.API.Controllers.Base;
using Reservation.Common.Core;
using Reservation.Common.Parameters;
using Reservation.Identity.Service.Core;
using Reservation.Identity.Service.Interfaces;

namespace Reservation.API.Controllers.Secuirty
{
    /// <inheritdoc />
    [Route("[controller]")]
    [ApiController] 
    [Authorize]
    public class UserController : BaseMainController
    {
        private readonly IUserServices _userServices;
        /// <inheritdoc />
        public UserController(IHandlerResponse responseHandler, ITokenBusiness tokenBusiness, IUserServices userServices) : base(responseHandler, tokenBusiness)
        {
            _userServices = userServices;
        }
        /// <summary>
        /// Get Users Select2
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet("GetUsersSelect2")]
        public async Task<IActionResult> GetUsersSelect2(int pageSize, int pageNumber, string searchTerm = null)
        {
            return  Ok(await _userServices.GetUsersSelect2(searchTerm, pageSize, pageNumber));
        }
        /// <summary>
        /// Get users to assigned to Role 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetUserAssigned/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUserAssigned(string id)
        {
            return Ok(await _userServices.GetUserAssignedSelect2(id));
        }
        /// <summary>
        /// Save User assigned
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [Route("SaveUserAssigned")]
        [HttpPost]
        public async Task<IResult> SaveUserAssigned(AssignUserOnRoleParameters parameters)
        {
            var repositoryResult = await _userServices.SaveUserAssigned(parameters);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
    }
}