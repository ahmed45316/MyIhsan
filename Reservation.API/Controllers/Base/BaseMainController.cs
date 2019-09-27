using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyIhsan.Common.Core;
using MyIhsan.Identity.Service.Core;

namespace MyIhsan.API.Controllers.Base
{
    /// <inheritdoc />
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
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