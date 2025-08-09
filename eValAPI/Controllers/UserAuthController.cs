using eValDTO.DTOs;
using eValService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eValAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private IUserAuthService _userauthService { get; set; }
        public UserAuthController(IUserAuthService userauthService)
        {
            this._userauthService = userauthService;
        }
        [HttpPost("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(LoginRequest objLoginRequest)
        { ;
            try
            {
                LoginResponse objLoginResponse = new LoginResponse();
                if (objLoginRequest != null && !string.IsNullOrEmpty(objLoginRequest.UserName) && !string.IsNullOrEmpty(objLoginRequest.Password))
                {
                    objLoginResponse.UserName = objLoginRequest.UserName;
                    objLoginResponse.StatusCode = 200;
                    objLoginResponse.Message = "Login Suceed";
                    objLoginResponse.IsSuceed = true;
                    return Ok(objLoginResponse);
                }
                return NotFound(new LoginResponse { Message = "UserName or Password is Required", StatusCode = 404, IsSuceed = false});
            }
            catch (Exception ex)
            {
                return BadRequest(new LoginResponse { Message = $"Error Occured - {ex.Message}", StatusCode = 500, IsSuceed = false });
            }
        }
    }
}
