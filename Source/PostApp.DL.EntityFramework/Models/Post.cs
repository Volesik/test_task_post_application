using PostApp.DL.Abstraction.Models;

namespace PostApp.DL.EntityFramework.Models;

public class Post : BaseEnity
{
    public int UserId { get; set; }
    
    public virtual User User { get; set; } = null!;
    
    public string Title { get; set; }
    
    public string Body { get; set; }
}