using System.ComponentModel.DataAnnotations;

namespace AnalizaEvaluarilor.Common.DTO;

public class UserDTO
{
    [Required(ErrorMessage = "Email is required"),EmailAddressAttribute(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required"),MinLength(6,ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; }
    

    
    
}