using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyIhsan.API.Controllers.Base;
using MyIhsan.Common.Core;
using MyIhsan.Common.Parameters;
using MyIhsan.Identity.Service.Core;
using MyIhsan.Identity.Service.Dtos;
using MyIhsan.Identity.Service.Interfaces;

namespace MyIhsan.API.Controllers.Secuirty
{
    /// <inheritdoc />    
    public class UserController : BaseMainController
    {
        private readonly IUserServices _userServices;
        /// <inheritdoc />
        public UserController(IHandlerResponse responseHandler, ITokenBusiness tokenBusiness, IUserServices userServices) : base(responseHandler, tokenBusiness)
        {
            _userServices = userServices;
        }
        /// <summary>
        /// Get Users
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IDataPagging> GetAll(GetAllUserParameters parameters)
        {
            var repositoryResult = await _userServices.GetUsers(parameters);
            return repositoryResult;
        }
        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IResult> Get(long Id)
        {
            var repositoryResult = await _userServices.GetUser(Id);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Add(UserDto userDto)
        {
            var repositoryResult = await _userServices.AddUser(userDto);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IResult> Update(UserDto userDto)
        {
            var repositoryResult = await _userServices.UpdateUser(userDto);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Remove User by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IResult> Remove(long id)
        {
            var repositoryResult = await _userServices.RemoveUserById(id);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
        /// <summary>
        /// Check Fields IsExists
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpGet("{name}/{type}/{id?}")]
        //public async Task<IResult> IsExists(string name, byte type, string id = null)
        //{
        //    var repositoryResult = type == 3 ? await _userServices.IsPhoneExists(name, id) : type == 2 ? await _userServices.IsEmailExists(name, id) : await _userServices.IsUsernameExists(name, id);
        //    var result = ResponseHandler.GetResult(repositoryResult);
        //    return result;
        //}

    }
}