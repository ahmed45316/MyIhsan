using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.Common.Core;
using Reservation.Identity.Service.Core;

namespace Reservation.API.Controllers.Base
{
    /// <inheritdoc />
    [Route("[controller]")]
    [ApiController]
    public class BaseMainController : ControllerBase
    {
        /// <inheritdoc />
        protected readonly ITokenBusiness TokenBusiness;
        /// <inheritdoc />
        protected readonly IHandlerResponse ResponseHandler;
        /// <inheritdoc />
        public BaseMainController(IHandlerResponse responseHandler)
        {
            ResponseHandler = responseHandler;
        }
        /// <inheritdoc />
        public BaseMainController(IHandlerResponse responseHandler, ITokenBusiness tokenBusiness)
        {
            ResponseHandler = responseHandler;
            TokenBusiness = tokenBusiness;
        }
    }
}