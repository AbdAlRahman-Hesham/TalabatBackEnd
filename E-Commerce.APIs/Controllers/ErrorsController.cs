using E_Commerce.DTOs.ErrorResponse;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Controllers;

[Route("errors/{code}")]
[ApiController]
public class ErrorsController : ControllerBase
{
    public ActionResult Error(int code) {
        return NotFound(new ApiResponse(code,"Not found end point"));
    }
}
