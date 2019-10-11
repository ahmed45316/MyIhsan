using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyIhsan.API.Controllers.Base;
using MyIhsan.Common.Core;
using MyIhsan.Common.Parameters;
using MyIhsan.Service.Core;
using MyIhsan.Service.Interfaces;

namespace MyIhsan.API.Controllers.Secuirty
{
    /// <inheritdoc />
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
        [HttpPost]
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
        [HttpGet]
        public async Task<IResult> GetMenu()
        {
            var userId = User.Claims.First(t => t.Type == "UserId").Value;
            var repositoryResult = await _menuServices.GetMenu(userId);
            var result = ResponseHandler.GetResult(repositoryResult);
            return result;
        }
    }
}