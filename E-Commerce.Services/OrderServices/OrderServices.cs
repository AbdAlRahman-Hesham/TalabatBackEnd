using E_Commerce.Domain.Entities.OrderEntiti;
using E_Commerce.Domain.Entities.OrderEntities;
using E_Commerce.Domain.ServicesInterfaces;

namespace E_Commerce.Services.OrderServices;

internal class OrderServices : IOrderServices
{
    public Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        throw new NotImplementedException();
    }
}
