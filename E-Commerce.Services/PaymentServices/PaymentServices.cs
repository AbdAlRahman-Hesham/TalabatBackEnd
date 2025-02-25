using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.OrderEntiti;
using E_Commerce.Domain.Entities.OrderEntities;
using E_Commerce.Domain.ServicesInterfaces;
using E_Commerce.Repository.Reprositories_Interfaces;
using E_Commerce.Repository.Specifications;
using E_Commerce.Repository.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = E_Commerce.Domain.Entities.Product;

namespace E_Commerce.Services.PaymentServices;

public class PaymentServices(IUnitOfWork unitOfWork,IBasketRepository basketRepository,IConfiguration configuration) : IPaymentServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly IConfiguration _configuration = configuration;

    public async Task<UserBasket?> CreateOrUpdatePaymentIntentId(string basketId)
    {
        var basket = await _basketRepository.GetBasketAsync(basketId);
        
        if (basket == null) return null;

        if (basket.Items.Count > 0)
        {
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                
                if (product == null) return null;

                if (product.Price != item.Price) item.Price = product.Price;
            }
        }
        else
            return null;

        if(basket.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
            
            if (deliveryMethod == null) return null;
            
            basket.ShippingPrice = deliveryMethod.Cost; 
        }

        StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];
        
        PaymentIntentService service = new PaymentIntentService();



        PaymentIntent paymentIntent;

        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
            PaymentIntentCreateOptions options = new PaymentIntentCreateOptions
            {
                Amount = (long) basket.Items.Sum(i => i.Price * i.Quantity ) * 100 + (long)basket.ShippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };

            paymentIntent = await service.CreateAsync(options);
            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;
        }
        else
        {
            PaymentIntentUpdateOptions options = new PaymentIntentUpdateOptions() {
                Amount = (long)basket.Items.Sum(i => i.Price * i.Quantity) * 100 + (long)basket.ShippingPrice * 100
            };
            paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, options);
        }

        return await _basketRepository.UpdateBasketAsync(basket);

    }

    public async Task<Order?> UpdatePaymentIntentAsync(string paymentIntentId, bool IsSucceded)
    {
        OrderSpecification orderSpecification = new OrderSpecification(o=>o.PaymentIntentId == paymentIntentId);

        var order = await _unitOfWork.Repository<Order>().GetAsyncWithSpecification(orderSpecification);
        
        if (order == null) return null;

        if (IsSucceded)
            order.Status = OrderStatus.PaymentReceived;
        else
            order.Status = OrderStatus.PaymentFailed;

        return order;
    }
}
