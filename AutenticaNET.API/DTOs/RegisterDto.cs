using System.ComponentModel.DataAnnotations;

namespace AutenticaNET.API.DTOs;

public class RegisterDto
{
    [Required, StringLength(100, MinimumLength = 3)]
    public string Username { get; set; } = null!;

    [Required, EmailAddress, StringLength(254)]
    public string Email { get; set; } = null!;

    [Required, MinLength(8), MaxLength(128)]
    public string Password { get; set; } = null!;
}
