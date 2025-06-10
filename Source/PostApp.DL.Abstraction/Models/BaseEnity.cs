using PostApp.DL.Abstraction.Interfaces;

namespace PostApp.DL.Abstraction.Models;

public class BaseEnity : IBaseEntity
{
    public long Id { get; set; }
    
    public DateTimeOffset CreatedWhen { get; set; }
    
    public DateTimeOffset UpdatedWhen { get; set; }
}