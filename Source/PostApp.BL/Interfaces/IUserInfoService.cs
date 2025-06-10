using PostApp.DL.EntityFramework.Models;

namespace PostApp.BL.Interfaces;

public interface IUserInfoService
{
    Task<User[]> GetAsync(string partialCityName, CancellationToken token);
    
    Task UpsertAsync(User user, CancellationToken token);
}