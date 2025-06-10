using PostApp.BL.Interfaces;
using PostApp.DL.EntityFramework.Models;
using PostApp.DL.Interfaces;
using PostApp.DL.Specifications.User;

namespace PostApp.BL.Services;

public class UserInfoService : IUserInfoService
{
    private readonly IDatabaseContextRepository<User> _userRepository;
    
    public UserInfoService(IDatabaseContextRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User[]> GetAsync(string partialCityName, CancellationToken token)
    {
        var specification = new SearchUserInfoByCity(partialCityName);
        var result = await _userRepository.GetArrayAsync(
            specification,
            token,
            new[] { nameof(User.Posts) });

        return result;
    }

    public async Task UpsertAsync(User user, CancellationToken token)
    {
        var specification = new FindUserInfoByEmail(user.Email);
        var isUserExist = await _userRepository.AnyAsync(specification, token);
        if (isUserExist)
        {
            await _userRepository.UpdateAsync(
                specification,
                entity =>
                {
                    entity.UserName = user.UserName;
                    entity.City = user.City;
                    entity.Name = user.Name;
                },
                token);

            return;
        }
        
        await _userRepository.AddAsync(user, token);
    }
}