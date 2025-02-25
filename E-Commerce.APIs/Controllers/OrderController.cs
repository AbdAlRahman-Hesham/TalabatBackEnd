using E_Commerce.Domain.Entities.OrderEntiti;
using E_Commerce.Domain.Entities.OrderEntities;
using E_Commerce.Domain.ServicesInterfaces;
using E_Commerce.DTOs.ErrorResponse;
using E_Commerce.DTOs.OrderDtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.APIs.Controllers;

[Authorize]
public class OrdersController(IOrderServices orderServices) : BaseApiController
{
    private readonly IOrderServices orderServices = orderServices;

    [AllowAnonymous]
    [HttpGet("deliveryMethods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
        var deliveryMethods = await orderServices.GetDeliveryMethodsAsync();
        return Ok(deliveryMethods);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 401)]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
    {
        var email = User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.Email)?.Value!;
        //if (string.IsNullOrEmpty(email)) return Unauthorized(new ApiResponse(401, "Email not found"));
        var orders = await orderServices.GetOrdersForUserAsync(email);
        return Ok(orders.Adapt<IReadOnlyList<OrderToReturnDto>>());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrderToReturnDto), 200)]
    [ProducesResponseType(typeof(ApiResponse), 404)]
    [ProducesResponseType(typeof(ApiResponse), 401)]
    public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id)
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
        //if (string.IsNullOrEmpty(email)) return Unauthorized(new ApiResponse(401, "Email not found"));

        var order = await orderServices.GetOrderByIdAsync(id, email);
        if (order == null) return NotFound(new ApiResponse(404, "Order not found"));
        return Ok(order.Adapt<OrderToReturnDto>());
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderToReturnDto),200)]
    [ProducesResponseType(typeof(ApiResponse),401)]
    [ProducesResponseType(typeof(ApiResponse),400)]
    public async Task<ActionResult<OrderToReturnDto>> CreatOrder(OrderDto orderDto)
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
        //if (string.IsNullOrEmpty(email)) return Unauthorized(new ApiResponse(401, "Email not found"));

        var result = await orderServices.CreateOrderAsync(email,orderDto.DeliveryMethodId!.Value,orderDto.BasketId,orderDto.ShippingAddress.Adapt<Address>());

        if (result == null) return BadRequest(new ApiResponse(400, "Problem creating order check input body"));

        return Ok(result.Adapt<OrderToReturnDto>());
    }
}
