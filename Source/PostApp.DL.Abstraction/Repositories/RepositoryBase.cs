using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PostApp.DL.Abstraction.Extensions;
using PostApp.DL.Abstraction.Interfaces;
using PostApp.DL.Abstraction.Models;
using PostApp.DL.Abstraction.Specifications;
using PostApp.Enums;

namespace PostApp.DL.Abstraction.Repositories;

public abstract class RepositoryBase<TDbContext> : IRepositoryBase<TDbContext>
    where TDbContext : DbContext
{
    public TDbContext Context { get; }

    protected virtual IQueryable<TEntity> Set<TEntity>()
        where TEntity : BaseEnity
    {
        return Context.Set<TEntity>();
    }

    protected RepositoryBase(TDbContext context)
    {
        Context = context;
    }

    public virtual async Task<long> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        where TEntity : BaseEnity
    {
        var entry = Context.Add(entity);

        await SaveChangesAsync(cancellationToken);

        return entry.Entity.Id;
    }

    public Task<bool> AnyAsync<TEntity>(
            Specification<TEntity> specification,
            CancellationToken cancellationToken,
            IEnumerable<string>? includedProperties = null,
            bool noTracking = true,
            bool asSplitQuery = false)
        where TEntity : BaseEnity
    {
        var query = (IQueryable<TEntity>)Set<TEntity>()
            .IncludeAll(includedProperties);

        if (asSplitQuery)
        {
            query = query.AsSplitQuery();
        }

        return query.AnyAsync(specification.Expression, cancellationToken);
    }

    public Task<TEntity?> FirstOrDefaultAsync<TEntity>(
        Specification<TEntity> specification,
        CancellationToken cancellationToken,
        IEnumerable<string>? includedProperties = null,
        bool noTracking = true,
        bool asSplitQuery = false,
        IEnumerable<Expression<Func<TEntity?, object>>>? sortingExpressions = null,
        SortingOrder? sortingOrder = null)
        where TEntity : BaseEnity
    {
        var query = Set<TEntity>().GetFilteredQueryWithSorting(
            specification, includedProperties ?? Array.Empty<string>(), noTracking, asSplitQuery, sortingExpressions: sortingExpressions, sortingOrder: sortingOrder);

        return query.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<TEntity[]> GetArrayAsync<TEntity>(
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
        where TEntity : BaseEnity
    {
        var query = (IQueryable<TEntity>)Set<TEntity>().GetFilteredQueryWithSorting(
            specification, includedProperties ?? Array.Empty<string>(), noTracking, asSplitQuery, skip, take, sortingExpressions, sortingOrder);

        return query.ToArrayAsync(cancellationToken);
    }

    public async Task UpdateAsync<TEntity>(
        Specification<TEntity> specification,
        Action<TEntity> updateAction,
        CancellationToken cancellationToken,
        IEnumerable<string>? includedProperties = null)
        where TEntity : BaseEnity
    {
        var originalEntity = await FirstOrDefaultAsync(specification, cancellationToken, includedProperties, false)
            ?? throw new Exception($"Could not find entity {typeof(TEntity).Name} by specification {specification.GetType().Name}");

        updateAction(originalEntity);

        await SaveChangesAsync(cancellationToken);
    }

    private Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;

        var modifiedAuditedEntities = Context.ChangeTracker
            .Entries<IBaseEntity>()
            .Where(entity => entity.State == EntityState.Modified)
            .Select(entity => entity.Entity)
            .ToList();

        modifiedAuditedEntities.ForEach(entity =>
        {
            entity.UpdatedWhen = now;
        });

        return Context.SaveChangesAsync(cancellationToken);
    }
}