using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using test.Models;
using test.Services;

namespace test.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly SchemaTestContext _dbo;

        public AuthService(IConfiguration configuration, SchemaTestContext dbo)
        {
            _configuration = configuration;
            _dbo = dbo;
        }

        public string GenerateJwtToken(string email, int RoleId)
        {
            var Rolename = _dbo.Roles.Where(r => r.Roleid == RoleId).Select(r => r.Rolename).FirstOrDefault();
            // var roleName = _dbo.Roles.Where(Role)
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, Rolename)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}


