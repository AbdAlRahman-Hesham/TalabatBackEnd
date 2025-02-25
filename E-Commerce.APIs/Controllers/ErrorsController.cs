﻿using E_Commerce.DTOs.ErrorResponse;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Controllers;

[Route("errors/{code}")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    public ActionResult Error(int code) {
        return code switch
        {
            400 => BadRequest(new ApiResponse(code, "A bad request, you have made")),
            401 => Unauthorized(new ApiResponse(code, "Unauthorized")),
            403 => Forbid("Forbidden"),
            404 => NotFound(new ApiResponse(code, "Resource not found")),
            500 => StatusCode(500, new ApiResponse(code, "An internal server error occurred")),
            _ => StatusCode(code, new ApiResponse(code, "An unexpected error occurred"))
        };

    }
}
