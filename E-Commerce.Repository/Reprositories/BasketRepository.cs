using E_Commerce.Domain.Entities;
using E_Commerce.Repository.Reprositories_Interfaces;
using StackExchange.Redis;
using System.Text.Json;


namespace E_Commerce.Repository.Reprositories;

public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
{
    private readonly IConnectionMultiplexer _connectionMultiplexer = connectionMultiplexer;
    private  IDatabase _db => _connectionMultiplexer.GetDatabase();



    public async Task<bool> DeleteBasketAsync(string basketId)
    {
       return await _db.KeyDeleteAsync(basketId);
    }

    public async Task<UserBasket?> GetBasketAsync(string basketId)
    {
        var basket = await _db.StringGetAsync(basketId);
        return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<UserBasket>(basket!);
    }

    public async Task<UserBasket?> UpdateBasketAsync(UserBasket basket)
    {
        var createdOrUpdated = await _db.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
        if (!createdOrUpdated)
            return null;
        return await GetBasketAsync(basket.Id);
    }
}
