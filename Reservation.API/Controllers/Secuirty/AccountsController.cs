using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.API.Controllers.Base;
using Reservation.Common.Core;
using Reservation.Common.IdentityInterfaces;
using Reservation.Common.Parameters;
using Reservation.Identity.Entities.Entities;
using Reservation.Identity.Service.Core;
using Reservation.Identity.Service.Interfaces;

namespace Reservation.API.Controllers.Secuirty
{
    /// <inheritdoc />
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : BaseMainController
    {
        private readonly ILoginServices _loginServices;
        private readonly IMenuServices _menuServices;
        /// <inheritdoc />
        public AccountsController(IHandlerResponse responseHandler,
            ILoginServices loginServices,
            ITokenBusiness tokenBusiness, IMenuServices menuServices)
            : base(responseHandler, tokenBusiness)
        {
            _loginServices = loginServices;
            _menuServices = menuServices;
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost(nameof(Login))]
        [AllowAnonymous]
        public async Task<IResult> Login(LoginParameters parameter)
        {
            var repositoryResult = await _loginServices.Login(parameter);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Get Menu
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetMenu))]
        [Authorize]
        public async Task<IResult> GetMenu()
        {
            var userId = User.Claims.First(t => t.Type == "UserId").Value;
            var repositoryResult = await _menuServices.GetMenu(userId);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
    }
}