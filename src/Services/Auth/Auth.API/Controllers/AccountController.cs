using Auth.API.Data.Models.Requests;
using Auth.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController:ControllerBase
    {
        private IAccountService _accountService;
        private ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost("/Register")]
        public IActionResult Register([FromBody] RegisterModel registerModel)
        {
            var result = _accountService.Register(registerModel);
            if (result.Successful)
            {
                var token = _tokenService.CreateToken(result.Value);
                Response.Headers.Append("Authorization", $"Bearer {token}");
                return Ok();
            }
            else
                return BadRequest(result.Message);
        }

        [HttpPost("/Login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var result = _accountService.Login(loginModel);
            if (result.Successful)
            {
                var token = _tokenService.CreateToken(result.Value);
                Response.Headers.Append("Authorization", $"Bearer {token}");
                return Ok();
            }
            else
                return BadRequest(result.Message);
        }
    }
}
