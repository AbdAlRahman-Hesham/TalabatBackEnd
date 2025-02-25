using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.OrderEntiti;
using E_Commerce.Domain.ServicesInterfaces;
using E_Commerce.DTOs.ErrorResponse;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_Commerce.APIs.Controllers;

public class PaymentsController(IPaymentServices paymentServices, IOrderServices orderServices,
    ILogger<PaymentsController> logger) : BaseApiController
{
    private readonly IPaymentServices _paymentServices = paymentServices;
    private readonly IOrderServices _orderServices = orderServices;
    private readonly ILogger<PaymentsController> _logger = logger;

    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<UserBasket>> CreateOrUpdatePaymentIntentId(string basketId)
    {
        var basket = await _paymentServices.CreateOrUpdatePaymentIntentId(basketId);

        if (basket == null) return BadRequest(new ApiResponse(400, "Problem with your basket"));

        return basket;
    }
    [HttpPost("/webhook")]
    public async Task<IActionResult> Index()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        var stripeEvent = EventUtility.ParseEvent(json);

        var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
        Order? order = null;
        // Handle the event
        // If on SDK version < 46, use class Events instead of EventTypes
        if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
        {
            order = await _paymentServices.UpdatePaymentIntentAsync(paymentIntent!.Id, true);
            _logger.LogInformation("Payment Succeeded PaymentIntent:- {0}", paymentIntent.Id);
        }
        else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
        {
            order = await _paymentServices.UpdatePaymentIntentAsync(paymentIntent!.Id, false);
            _logger.LogInformation("Payment Failed PaymentIntent:- {0}", paymentIntent.Id);
        }
        else
        {
            _logger.LogWarning("Unhandled event type: {0}", stripeEvent.Type);
        }
        if (order is null)
            _logger.LogWarning("No Order With PaymentIntent:- {0}", paymentIntent!.Id);



        return Ok();
    }
}
