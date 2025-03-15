using System.ComponentModel.DataAnnotations;

namespace test.ViewModel;

public class AddUserViewModel
{
    [Required(ErrorMessage = "First Name is required")]
    public string? Firstname { get; set; }

    [Required]
    public string? Lastname { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required, EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public string? Phone { get; set; }


    public int RoleId { get; set; }


    public string? Country { get; set; }


    public string? State { get; set; }

    public string? City { get; set; }

    public int Zipcode { get; set; }

    public string? Address { get; set; }

    public string? Createdby { get; set; }
}
