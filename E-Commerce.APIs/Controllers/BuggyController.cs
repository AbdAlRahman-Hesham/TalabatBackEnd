using E_Commerce.Repository.Data;
using E_Commerce.DTOs.ErrorResponse;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_Commerce.APIs.Controllers;

public class BuggyController(StoreContext context) : BaseApiController
{
    private readonly StoreContext _context = context;

    [HttpGet("notfound")]
    public ActionResult GetNotFound()
    {
        return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));
    }

    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        var product = _context.Products.Find(21322);

        var result = product.ToString();
        return Ok(result);
    }
    
    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest();
    }

    [HttpGet("badrequest/{id}")]
    public ActionResult GetBadRequest(int id)
    {
        return Ok();
    }
}
