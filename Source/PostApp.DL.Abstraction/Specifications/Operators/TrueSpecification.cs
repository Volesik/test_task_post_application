using System.Linq.Expressions;
using PostApp.DL.Abstraction.Interfaces;

namespace PostApp.DL.Abstraction.Specifications.Operators;

public class TrueSpecification<T> : Specification<T>
    where T : IBaseEntity
{
    public override Expression<Func<T, bool>> Expression => True();
}
