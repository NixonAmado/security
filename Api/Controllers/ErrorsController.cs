using Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("errors/{code}")]
public class ErrorsController : BaseApiController
{
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}
