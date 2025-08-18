using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Models.DTOs;

namespace MyWebApiApp.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            Console.WriteLine(context?.Error);

            ApiResponse response = new ApiResponse(
                "Internal Server Error",
                StatusCodes.Status500InternalServerError
            );

            return StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}