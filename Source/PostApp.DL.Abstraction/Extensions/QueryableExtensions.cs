using System.Linq.Expressions;
using PostApp.DL.Abstraction.Models;
using PostApp.DL.Abstraction.Specifications;

namespace PostApp.DL.Abstraction.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntity?> GetFilteredQueryWithoutSorting<TEntity>(
        this IQueryable<TEntity?> query,
        Specification<TEntity> specification,
        int skip = default,
        int take = default)
        where TEntity : BaseEnity
    {
        query = query.GetFilteredQuery(specification);

        if (skip != default)
        {
            query = query.Skip(skip);
        }

        if (take != default)
        {
            query = query.Take(take);
        }

        return query;
    }

    public static IQueryable<TEntity?> GetFilteredQuery<TEntity>(
        this IQueryable<TEntity?> query,
        Specification<TEntity> specification)
        where TEntity : BaseEnity
    {
        query = query.DefaultIfEmpty();

        var expression = specification.Expression as Expression<Func<TEntity?, bool>>;
        query = query.Where(expression);

        return query;
    }

}