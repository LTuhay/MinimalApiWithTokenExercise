using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MinimalApiWithToken.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/welcome")]
    public class WelcomeController : ControllerBase
    {

        [HttpGet]

        public ActionResult<String> Welcome()
        {
            String welcome = "Welcome!";
            return Ok(welcome);
        }


    }
}
