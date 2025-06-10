using PostApp.DL.EntityFramework.Models;

namespace PostApp.BL.Interfaces;

public interface IPostService
{
    Task UpsertAsync(Post post, CancellationToken token);
}