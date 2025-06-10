using PostApp.DL.Abstraction.Models;

namespace PostApp.DL.Interfaces;

public interface IDatabaseContextRepository<T> : IDatabaseContextRepository
    where T : BaseEnity
{
}