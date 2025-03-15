using System.ComponentModel.DataAnnotations;

namespace test.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    // [Required(ErrorMessage = "Email is required")]
    // [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;

    [Required]
    // [Required(ErrorMessage = "Password is required")]
    // [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    // [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    //     ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character (@$!%*?&)")]
    public string password{ get;set;}

    public bool RememberMe { get; set; }

}

