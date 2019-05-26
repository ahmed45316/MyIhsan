using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.Common.Core;
using Reservation.Identity.Service.Core;

namespace Reservation.API.Controllers.Base
{
    /// <inheritdoc />
    [Route("[controller]")]
    [ApiController]
    public class MainController<TEntity,TDto> : ControllerBase
    {
        private readonly IBaseService<TEntity, TDto> _baseService;
        /// <inheritdoc />
        protected readonly ITokenBusiness TokenBusiness;
        /// <inheritdoc />
        protected readonly IHandlerResponse ResponseHandler;
        /// <inheritdoc />
        public MainController(IHandlerResponse responseHandler, IBaseService<TEntity, TDto> baseService)
        {
            ResponseHandler = responseHandler;
            _baseService = baseService;
        }
        /// <inheritdoc />
        public MainController(IHandlerResponse responseHandler, ITokenBusiness tokenBusiness, IBaseService<TEntity, TDto> baseService)
        {
            ResponseHandler = responseHandler;
            TokenBusiness = tokenBusiness;
            _baseService = baseService;
        }
        /// <summary>
        /// Add Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(nameof(Add))]
        [Authorize]
        public async Task<IResult> Add(TDto model)
        {
            var repositoryResult = await _baseService.AddAsync(model);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Get all Data
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(GetAll))]
        [Authorize]
        public async Task<IResult> GetAll()
        {
            var repositoryResult = await _baseService.GetAllAsync();
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
    }
}