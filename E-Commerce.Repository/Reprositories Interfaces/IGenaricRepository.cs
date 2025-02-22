using E_Commerce.Domain.Entities;
using E_Commerce.Repository.Specifications;
using System.Linq.Expressions;

namespace E_Commerce.Repository.Reprositories_Interfaces;

public interface IGenaricRepository<T> where T : BaseEntity
{
    public Task<T?> GetAsync(int id);
    public Task<T?> GetAsyncWithSpecification(int id, Specification<T> specification);
    public Task<IReadOnlyList<T>> GetAllAsync();
    public Task<IReadOnlyList<T>> GetAllAsyncWithSpecification(Specification<T> specification);
    public Task<int> GetCountAsync(Expression<Func<T, bool>>? Criteria);
}
