using Microsoft.EntityFrameworkCore;
using PostApp.DL.Abstraction.Models;
using PostApp.DL.Abstraction.Specifications;

namespace PostApp.DL.Abstraction.Interfaces;

public interface IRepositoryBase<out TDbContext>
    where TDbContext : DbContext

{
    TDbContext Context { get; }

    Task<long> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        where TEntity : BaseEnity;
    
    Task<bool> AnyAsync<TEntity>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
        where TEntity : BaseEnity;
    
    Task<TEntity?> FirstOrDefaultAsync<TEntity>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken)
        where TEntity : BaseEnity;

    
    Task<TEntity[]> GetArrayAsync<TEntity>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken,
        int skip = default,
        int take = default)
        where TEntity : BaseEnity;
    
    Task UpdateAsync<TEntity>(
        Specification<TEntity> specification,
        Action<TEntity> updateAction,
        CancellationToken cancellationToken)
        where TEntity : BaseEnity;

    Task<int> CountAsync<TEntity>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken,
        bool asSplitQuery = false,
        bool useDistinct = false)
        where TEntity : BaseEnity;

}