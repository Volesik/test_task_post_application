using PostApp.DL.Abstraction.Interfaces;
using PostApp.DL.EntityFramework;

namespace PostApp.DL.Interfaces;

public interface IDatabaseContextRepository : IRepositoryBase<PostAppContext>
{
}