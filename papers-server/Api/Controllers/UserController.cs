using Papers.Api.Models;
using Papers.Common.Exceptions;

namespace Papers.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Papers.Common.Enums;
    using Papers.Common.Helpers;
    using Papers.Domain.Managers;
    using Papers.Domain.Models.User;

    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public SendResult Register([FromBody]RegisterDataModel registerData)
        {
            if (registerData == null)
            {
                throw new PapersBusinessException($"{nameof(registerData)} is empty");
            }

            this.userManager.Register(
                new UserInfo
                {
                    Login = registerData.Login,
                    FirstName = registerData.FirstName,
                    LastName = registerData.LastName,
                    UserPhone = registerData.Phone
                },
                registerData.Password);
            return SendResult.Success;
        }

        [HttpGet]
        [Route("{id}/state")]
        public string GetUserState(long id)
        {
            var user = this.userManager.GetById(id);
            return user == null ? "null" : user.State.ToString();
        }

        [HttpPost]
        [Route("confirm")]
        public SendResult Confirm(string phone, string code)
        {
            this.userManager.ConfirmUser(phone, code);
            return SendResult.Success;
        }

        [HttpGet]
        [Route("test")]
        public string TestGenerator(long id, string phone, string login)
        {
            return CommonExtensions.GenerateConfirmCode(id, phone, login);
        }
    }
}