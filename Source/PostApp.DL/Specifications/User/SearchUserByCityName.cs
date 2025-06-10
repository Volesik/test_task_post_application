using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PostApp.DL.Abstraction.Specifications;

namespace PostApp.DL.Specifications.User;

public class SearchUserInfoByFullName : Specification<EntityFramework.Models.User>
{
    private readonly string? _cityName;

    public SearchUserInfoByFullName(string? cityName)
    {
        _cityName = cityName;
    }

    public override Expression<Func<EntityFramework.Models.User, bool>> Expression =>
        result => EF.Functions.ILike(result.City, $"{_cityName}%");

}