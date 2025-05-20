using API.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected UnitOfWork _unitOfWork;
        public BaseController( UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
    }
}
