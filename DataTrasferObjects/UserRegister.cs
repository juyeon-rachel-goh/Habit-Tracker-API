using System.ComponentModel.DataAnnotations;

namespace Api.DataTransferObjects;
public class UserRegister
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]  
    public string Password { get; set; }
    
}

