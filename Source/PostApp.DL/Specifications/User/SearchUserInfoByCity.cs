using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PostApp.DL.Abstraction.Specifications;

namespace PostApp.DL.Specifications.User;

public class SearchUserInfoByCity : Specification<EntityFramework.Models.User>
{
    private readonly string? _cityName;

    public SearchUserInfoByCity(string? cityName)
    {
        _cityName = cityName;
    }

    public override Expression<Func<EntityFramework.Models.User, bool>> Expression =>
        result => EF.Functions.ILike(result.City, $"{_cityName}%");

}