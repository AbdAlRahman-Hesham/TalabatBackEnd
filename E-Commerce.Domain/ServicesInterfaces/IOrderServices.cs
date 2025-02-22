using E_Commerce.Domain.Entities.OrderEntiti;
using E_Commerce.Domain.Entities.OrderEntities;

namespace E_Commerce.Domain.ServicesInterfaces;

public interface IOrderServices
{
    Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress);
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
    //Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();


}
