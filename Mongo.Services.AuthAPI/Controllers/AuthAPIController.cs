using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mongo.Services.AuthAPI.Models.DTOs;
using Mongo.Services.AuthAPI.Service.IService;

namespace Mongo.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {

        public readonly IAuthService _authService;
        public ResponseDTO _responseDTO;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDTO = new();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            var errorMessage = await _authService.Register(registrationRequestDTO);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = errorMessage;
                return BadRequest(_responseDTO);
            }
            return Ok(_responseDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _authService.Login(loginRequestDTO);
            if (loginResponse.User == null)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "User name or password is incorrect";
                return BadRequest(_responseDTO);
            }
            _responseDTO.Result = loginResponse;
            return Ok(_responseDTO);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDTO model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email,model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = "User name or password is incorrect";
                return BadRequest(_responseDTO);
            }
            return Ok(_responseDTO);
        }
    }
}
