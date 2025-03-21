using System.ComponentModel.DataAnnotations;

public class UserDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    // Add Role to support Admin/User registration
    public string Role { get; set; } = "User";  // Default role is "User"
}
