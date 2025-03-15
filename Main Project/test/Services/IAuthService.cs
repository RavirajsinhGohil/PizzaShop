namespace test.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, int RoleId);

    }
}

