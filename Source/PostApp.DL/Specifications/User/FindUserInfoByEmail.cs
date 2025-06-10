using System.Linq.Expressions;
using PostApp.DL.Abstraction.Specifications;

namespace PostApp.DL.Specifications.User;

public class FindUserInfoByEmail : Specification<EntityFramework.Models.User>
{
    private readonly string? _email;

    public FindUserInfoByEmail(string? email)
    {
        _email = email;
    }

    public override Expression<Func<EntityFramework.Models.User, bool>> Expression =>
        result => result.Email == _email;

}