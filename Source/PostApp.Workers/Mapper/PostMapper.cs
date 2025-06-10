using PostApp.DL.EntityFramework.Models;
using PostApp.Web.Common.Models;

namespace PostApp.Workers.Mapper;

public class PostMapper
{
    public Post ToPost(PostResponseModel postResponseModel)
    {
        return new Post
        {
            Title = postResponseModel.Title,
            UserId = postResponseModel.UserId,
            Body = postResponseModel.Body,
        };
    }
}