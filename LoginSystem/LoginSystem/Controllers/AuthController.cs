using LoginSystem.Data;
using LoginSystem.DTOs;
using LoginSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginSystem.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly SSODbContext _db;
        private readonly IConfiguration _config;

        public AuthController(SSODbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        [HttpPost("login")]
        public ActionResult<TokenResponse> Login(LoginRequest request)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == request.Username);

            if (user == null || user.PasswordHash != request.Password)
                return Unauthorized("Invalid credentials");

            var role = _db.Roles.FirstOrDefault(r => r.Id == user.RoleId);
            var permissions = _db.RolePermissions
                                .Where(rp => rp.RoleId == role.Id)
                                .Select(rp => rp.Permission.Action)
                                .ToList();

            var appInfo = _db.Applications.FirstOrDefault(a => a.Id == role.ApplicationId);

            var claims = new List<Claim>
            {
                new Claim("username", user.Username),
                new Claim("user_ID", user.Id.ToString()),
                new Claim("role", role?.Name ?? ""),
                new Claim("application_code", appInfo?.Code ?? ""),
                new Claim("application_name", appInfo?.Name ?? "")
            };

            foreach (var perm in permissions)
                claims.Add(new Claim("permission", perm));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenResponse { AccessToken = jwt };
        }
    }
}
