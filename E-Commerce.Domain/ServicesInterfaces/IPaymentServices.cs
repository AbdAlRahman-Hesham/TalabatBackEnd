using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.OrderEntiti;

namespace E_Commerce.Domain.ServicesInterfaces;

public interface IPaymentServices
{
    Task<UserBasket?> CreateOrUpdatePaymentIntentId(string basketId);
    Task<Order?> UpdatePaymentIntentAsync(string paymentIntentId, bool IsSucceded);

}
