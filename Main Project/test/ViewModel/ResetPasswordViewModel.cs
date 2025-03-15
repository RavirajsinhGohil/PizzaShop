namespace test.ViewModel;

public class ResetPasswordViewModel
{
    public string Email { get; set; } = "abc@example.com";
    public string NewPassword { get; set; }

    public string ConfirmPassword { get; set; }
}
