using Microsoft.AspNetCore.Mvc;
using MinimalApiWithToken.Users;

namespace MinimalApiWithToken.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtGenerator;
        public AuthController(JwtTokenGenerator tokenGenerator)
        {
            _jwtGenerator = tokenGenerator;
        }

        [HttpPost]
        public ActionResult<String> Login(UserDTO userDto)
        {
            var token = _jwtGenerator.GenerateToken(userDto.UserName, userDto.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
