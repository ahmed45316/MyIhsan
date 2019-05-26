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
        /// <inheritdoc />
        public AccountsController(IHandlerResponse responseHandler,
            ILoginServices loginServices,
            ITokenBusiness tokenBusiness)
            : base(responseHandler, tokenBusiness)
        {
            _loginServices = loginServices;
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
    }
}