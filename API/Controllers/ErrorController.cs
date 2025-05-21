using API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octokit.Internal;
using Refit;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetError(int code)
        {
            return new ObjectResult(new ResponseAPI(code))
            {
                StatusCode = code
            };
        }
    }
}
