using PostApp.DL.Abstraction.Models;
using PostApp.DL.Abstraction.Repositories;
using PostApp.DL.EntityFramework;
using PostApp.DL.Interfaces;

namespace PostApp.DL.Repositories;

public class DatabaseContextRepository<T> : RepositoryBase<PostAppContext>, IDatabaseContextRepository<T>
    where T : BaseEnity
{
    public DatabaseContextRepository(PostAppContext context)
        : base(context)
    {
    }
}