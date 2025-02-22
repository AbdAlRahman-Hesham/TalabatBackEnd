using System.Runtime.Serialization;

namespace E_Commerce.Domain.Entities.OrderEntities;

public enum OrderStatus
{
    [EnumMember(Value = "Pending")]
    Pending,
    [EnumMember(Value = "Payment Received")]
    PaymentReceived,
    [EnumMember(Value = "Payment Failed")]
    PaymentFailed,
    [EnumMember(Value = "Shipped")]
    Shipped,
    [EnumMember(Value = " Delivered")]
    Delivered
}
