using System.ComponentModel.DataAnnotations;

namespace AnalizaEvaluarilor.Common.DTO;

public class RegistrDTO
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required"),
     MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Password confirmation is required"),
     Compare("Password", ErrorMessage = "Passwords do not match")]

    public string PasswordConfimr { get; set; }

    [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
}