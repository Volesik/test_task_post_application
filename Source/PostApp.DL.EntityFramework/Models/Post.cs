using PostApp.DL.Abstraction.Models;

namespace PostApp.DL.EntityFramework.Models;

public class Post : BaseEnity
{
    public string Title { get; set; }
    
    public string Body { get; set; }
    
    public int UserId { get; set; }
    
    public User User { get; set; } = null!;
}