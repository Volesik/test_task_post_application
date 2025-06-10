using PostApp.DL.Abstraction.Repositories;
using PostApp.DL.EntityFramework;
using PostApp.DL.Interfaces;

namespace PostApp.DL.Repositories;

public class DatabaseContextRepository : RepositoryBase<PostAppContext>, IDatabaseContextRepository
{
    public DatabaseContextRepository(PostAppContext context)
        : base(context)
    {
    }
}