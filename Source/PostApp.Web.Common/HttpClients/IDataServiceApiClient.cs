using PostApp.Web.Common.Models;
using Refit;

namespace PostApp.Web.Common.HttpClients;

public interface IDataServiceApiClient
{
    [Post("/users")]
    Task<UserResponseModel[]> GetUsersAsync();
    
    [Post("/posts")]
    Task<PostResponseModel[]> GetPostsAsync();
}