using E_Commerce.Domain.ServicesInterfaces;
using StackExchange.Redis;
using System.Text.Json;


namespace E_Commerce.Services.CacheServices;

public class CacheResponseService(IConnectionMultiplexer connectionMultiplexer) : ICacheResponseService
{
    private readonly IDatabase _db = connectionMultiplexer.GetDatabase();
    public async Task SetObjectInMemoryAsync(string key, object value, TimeSpan lifeTime)
    {
        if (value == null)
            return;

        var serializedValue = JsonSerializer.Serialize(value, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        
        
        });

        await _db.StringSetAsync(key, serializedValue, lifeTime);
    }
    
    public async Task<string?> GetObjectInMemoryAsync(string key)
    {
        var serializedValue = await _db.StringGetAsync(key);

        if (serializedValue.IsNullOrEmpty)
            return null;

        return serializedValue;
    }

}
