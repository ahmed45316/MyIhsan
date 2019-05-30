﻿using System;
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
    public class PageController : BaseMainController
    {
        private readonly IMenuServices _menuServices;
        /// <inheritdoc />
        public PageController(IHandlerResponse responseHandler, ITokenBusiness tokenBusiness, IMenuServices menuServices) : base(responseHandler, tokenBusiness)
        {
            _menuServices = menuServices;
        }
        /// <summary>
        /// Get Screens Select2
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="searchTerm"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpGet("GetScreensSelect2")]
        public async Task<IActionResult> GetScreensSelect2(int pageSize, int pageNumber, string searchTerm = null, string lang = "ar-EG")
        {
            return Ok(await _menuServices.GetScreensSelect2(searchTerm, pageSize, pageNumber, lang));
        }
        /// <summary>
        /// Get child Screens Select2
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="parentId"></param>
        /// <param name="searchTerm"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpGet("GetChildScreensSelect2")]
        public async Task<IActionResult> GetChildScreensSelect2(int pageSize, int pageNumber, string parentId, string searchTerm = null, string lang = "ar-EG")
        {
            return Ok(await _menuServices.GetChildScreensSelect2(searchTerm, pageSize, pageNumber, parentId, lang));
        }
        /// <summary>
        /// Get Screen NotSelected
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <param name="childId"></param>
        /// <returns></returns>
        [HttpGet("GetScreenData/{roleId}/{menuId?}/{childId?}")]
        public async Task<IResult> GetScreenData(string roleId, string menuId = null, string childId = null)
        {
            var repositoryResult = await _menuServices.GetScreens(roleId, menuId, childId);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Get Screen Selected
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuId"></param>
        /// <param name="childId"></param>
        /// <returns></returns>
        [HttpGet("GetScreenDataSelected/{roleId}/{menuId?}/{childId?}")]
        public async Task<IResult> GetScreenDataSelected(string roleId, string menuId = null, string childId = null)
        {
            var repositoryResult = await _menuServices.GetScreensSelected(roleId, menuId, childId);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Save Screens
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost("SaveScreens")]
        public async Task<IResult> SaveScreens([FromForm]ScreensAssignedParameters parameters)
        {
            var repositoryResult = await _menuServices.SaveScreens(parameters);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
    }
}