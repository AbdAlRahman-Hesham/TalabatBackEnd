using E_Commerce.Domain.Entities.OrderEntiti;
using System.Linq.Expressions;


namespace E_Commerce.Repository.Specifications;

public class OrderSpecification : Specification<Order>
{
    public OrderSpecification(): base(DefaultIncludes)
    {
    }

    
    public OrderSpecification(Expression<Func<Order, bool>> criteria)
        : base(DefaultIncludes,criteria)
    {
    }


    private static readonly List<Expression<Func<Order, object>>> DefaultIncludes = new() {o=>o.DeliveryMethod,o=>o.OrderItems};
    
}
