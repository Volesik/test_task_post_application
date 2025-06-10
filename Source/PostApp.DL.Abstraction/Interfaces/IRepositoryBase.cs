using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PostApp.DL.Abstraction.Models;
using PostApp.DL.Abstraction.Specifications;
using PostApp.Enums;

namespace PostApp.DL.Abstraction.Interfaces;

public interface IRepositoryBase<out TDbContext>
    where TDbContext : DbContext

{
    TDbContext Context { get; }

    Task<long> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        where TEntity : BaseEnity;

    Task<bool> AnyAsync<TEntity>(
            Specification<TEntity> specification,
            CancellationToken cancellationToken,
            IEnumerable<string>? includedProperties = null,
            bool noTracking = true,
            bool asSplitQuery = false)
        where TEntity : BaseEnity;

    Task<TEntity[]> GetArrayAsync<TEntity>(
            Specification<TEntity> specification,
            CancellationToken cancellationToken,
            IEnumerable<string>? includedProperties = null,
            bool noTracking = true,
            int skip = default,
            int take = default,
            IEnumerable<Expression<Func<TEntity?, object>>>? sortingExpressions = null,
            SortingOrder? sortingOrder = null,
            bool asSplitQuery = false,
            bool useDistinct = false)
        where TEntity : BaseEnity;

    Task UpdateAsync<TEntity>(
            Specification<TEntity> specification,
            Action<TEntity> updateAction,
            CancellationToken cancellationToken,
            IEnumerable<string>? includedProperties = null)
        where TEntity : BaseEnity;
}