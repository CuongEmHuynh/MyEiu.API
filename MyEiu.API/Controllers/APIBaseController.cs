using Microsoft.AspNetCore.Mvc;

namespace MyEiu.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class APIBaseController : ControllerBase
    {
        //[NonAction] //Set not Tracking http method
        //public ObjectResult StatusCodeResult(OperationResult result)
        //{
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return StatusCode(result.StatusCode, result.Message);
        //    }
        //}

    }
}
