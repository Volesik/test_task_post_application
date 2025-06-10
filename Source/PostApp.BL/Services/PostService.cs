using PostApp.BL.Interfaces;
using PostApp.DL.EntityFramework.Models;
using PostApp.DL.Interfaces;
using PostApp.DL.Specifications;

namespace PostApp.BL.Services;

public class PostService : IPostService
{
    private readonly IDatabaseContextRepository<Post> _postRepository;
    
    public PostService(IDatabaseContextRepository<Post> postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task UpsertAsync(Post post, CancellationToken token)
    {
        var specification = new FindEntitiesByIds<Post>(post.Id);
        var isUserExist = await _postRepository.AnyAsync(specification, token);
        if (isUserExist)
        {
            await _postRepository.UpdateAsync(
                specification,
                entity =>
                {
                    entity.UserId = post.UserId;
                    entity.Title = post.Title;
                    entity.Body = post.Body;
                },
                token);

            return;
        }
        
        await _postRepository.AddAsync(post, token);
    }
}