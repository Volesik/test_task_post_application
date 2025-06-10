using System.Linq.Expressions;
using PostApp.DL.Abstraction.Interfaces;
using PostApp.DL.Abstraction.Specifications.Operators;

namespace PostApp.DL.Abstraction.Specifications;

public abstract class Specification<T> where T : IBaseEntity
{
    public abstract Expression<Func<T, bool>> Expression { get; }

    public static Expression<Func<T, bool>> True()
    {
        return input => true;
    }

    public static Specification<T> operator &(Specification<T> spec1, Specification<T> spec2) => new AndSpecification<T>(spec1, spec2);

    public static Specification<T> operator |(Specification<T> spec1, Specification<T> spec2) => new OrSpecification<T>(spec1, spec2);

    public static Specification<T> operator !(Specification<T> spec1) => new NotSpecification<T>(spec1);

}