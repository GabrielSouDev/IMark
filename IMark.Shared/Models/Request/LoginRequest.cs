using System.ComponentModel.DataAnnotations;

namespace IMark.Shared.Models.Request;

public class LoginRequest
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

}
