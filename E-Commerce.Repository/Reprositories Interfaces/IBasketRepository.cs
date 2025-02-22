using E_Commerce.Domain.Entities;

namespace E_Commerce.Repository.Reprositories_Interfaces;

public interface IBasketRepository
{
    public Task<UserBasket?> GetBasketAsync(string basketId);
    public Task<UserBasket?> UpdateBasketAsync(UserBasket basket);
    public Task<bool> DeleteBasketAsync(string basketId);

}
