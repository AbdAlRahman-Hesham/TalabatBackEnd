using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.OrderEntiti;
using E_Commerce.Domain.Entities.OrderEntities;
using E_Commerce.Domain.ServicesInterfaces;
using E_Commerce.Repository.Reprositories;
using E_Commerce.Repository.Reprositories_Interfaces;
using E_Commerce.Repository.Specifications;
using E_Commerce.Repository.UnitOfWork;

namespace E_Commerce.Services.OrderServices;

public class OrderServices(IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IOrderServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly IGenaricRepository<Order> _orderRepository = unitOfWork.Repository<Order>();

    public async Task<Order?> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
    {
        IGenaricRepository<Product> _productRepository = _unitOfWork.Repository<Product>();
        IGenaricRepository<DeliveryMethod> _deliveryMethodRepository = _unitOfWork.Repository<DeliveryMethod>();
        // 1. Get Basket From Baskets
        var basket = await _basketRepository.GetBasketAsync(basketId);
        if (basket is null)
            return null;

        // 2. Get Selected Items From Basket From Products Repo
        ICollection<OrderItem> itemOrdereds = new List<OrderItem>();

        foreach (var item in basket.Items)
        {
            var product = await _productRepository.GetAsync(item.Id);
            if (product is not null)
            {
                var productItemOrdered = new ProductItemOrdered
                {
                    PictureUrl = product.PictureUrl,
                    ProductName = product.Name,
                    ProductId = product.Id,
                };

                var orderItem = new OrderItem(productItemOrdered, item.Price, item.Quantity);
                itemOrdereds.Add(orderItem);
            }
            else
                return null;
        }


        // 3. Calculate Subtotal
        decimal subtotal = itemOrdereds.Sum(i => i.Quantity * i.Price);

        // 4. Get Delivery Method Repo
        var deliveryMethod = await _deliveryMethodRepository.GetAsync(deliveryMethodId);
        if (deliveryMethod is null)
            return null;

        // 5. Create Order
        var order = new Order(buyerEmail, shippingAddress, deliveryMethod, itemOrdereds, subtotal);
        order.PaymentIntentId = basket.PaymentIntentId!;
        await _orderRepository.AddAsync(order);
        // 6. Save to database
        var result = await _unitOfWork.CompleteAsync();
        if (result <= 0)
            return null;
        return order;
    }
    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        OrderSpecification orderSpecification = new(o => o.BuyerEmail == buyerEmail);
        var orders = await _orderRepository.GetAllAsyncWithSpecification(orderSpecification);
        return orders;
    }

    public async Task<Order?> GetOrderByIdAsync(int id, string buyerEmail)
    {
        OrderSpecification orderSpecification = new(o => o.BuyerEmail == buyerEmail && o.Id == id);
        var order = await _orderRepository.GetAsyncWithSpecification(orderSpecification);
        return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        IGenaricRepository<DeliveryMethod> _deliveryMethodRepository = _unitOfWork.Repository<DeliveryMethod>();
        var result = await _deliveryMethodRepository.GetAllAsync();
        return result;
    }
}
