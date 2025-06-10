using System.Linq.Expressions;
using PostApp.DL.Abstraction.Models;
using PostApp.DL.Abstraction.Specifications;

namespace PostApp.DL.Specifications;

public class FindEntitiesByIds<T> : Specification<T>
    where T : BaseEnity
{
    private readonly IEnumerable<long> _ids;

    public FindEntitiesByIds(long id)
    {
        _ids = new[] { id };
    }

    public FindEntitiesByIds(IEnumerable<long> ids)
    {
        _ids = ids;
    }

    public override Expression<Func<T, bool>> Expression
    {
        get
        {
            return result => _ids.Contains(result.Id);
        }
    }
}
