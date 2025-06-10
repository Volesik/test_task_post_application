using PostApp.DL.EntityFramework.Models;
using PostApp.Web.Common.Models;

namespace PostApp.Workers.Mapper;

public class UserMapper
{
    public User ToUserInfo(UserResponseModel userResponseModel)
    {
        return new User
        {
            Id = userResponseModel.Id,
            Email = userResponseModel.Email,
            Name = userResponseModel.Name,
            City = userResponseModel.Address.City,
            UserName = userResponseModel.UserName
        };
    }
}