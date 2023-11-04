using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Api.Controllers;

public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}