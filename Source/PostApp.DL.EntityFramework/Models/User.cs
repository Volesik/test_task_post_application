using PostApp.DL.Abstraction.Models;

namespace PostApp.DL.EntityFramework.Models;

public class User : BaseEnity
{
    public string Name { get; set; }
    
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public string City { get; set; }
}