using Hangfire;
using PostApp.BL.Interfaces;
using PostApp.DL.EntityFramework.Models;
using PostApp.Web.Common.HttpClients;
using PostApp.Workers.Interfaces;
using PostApp.Workers.Mapper;

namespace PostApp.Workers.Workers;

public class PostReadWorker : IWorker
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IPostService _postService;
    private readonly PostMapper _postMapper;
    private readonly IDataServiceApiClient _dataServiceApiClient;
    
    public PostReadWorker(
        IBackgroundJobClient backgroundJobClient,
        IPostService postService,
        PostMapper postMapper,
        IDataServiceApiClient dataServiceApiClient)
    {
        _backgroundJobClient = backgroundJobClient;
        _postService = postService;
        _postMapper = postMapper;
        _dataServiceApiClient = dataServiceApiClient;
    }
    
    public async Task ExecuteAsync(int bulkSize)
    {
        try
        {
            var posts = await _dataServiceApiClient.GetPostsAsync();
            foreach (var post in posts)
            {
                var userInfo = _postMapper.ToPost(post);
                
                _backgroundJobClient.Enqueue(() => ProcessAsync(userInfo));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }
    
    public async Task ProcessAsync(Post post)
    {
        await _postService.UpsertAsync(post, CancellationToken.None);
    }
}