namespace PostApp.Web.Common.Models;

public class UserResponseModel
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string UserName { get; set; }

    public UserAddressResponseData Address { get; set; }
}