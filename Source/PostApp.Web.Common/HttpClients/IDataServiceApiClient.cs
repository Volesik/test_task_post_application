using PostApp.Web.Common.Models;
using Refit;

namespace PostApp.Web.Common.HttpClients;

public interface IDataServiceApiClient
{
    [Get("/users")]
    Task<UserResponseModel[]> GetUsersAsync();
    
    [Get("/posts")]
    Task<PostResponseModel[]> GetPostsAsync();
}