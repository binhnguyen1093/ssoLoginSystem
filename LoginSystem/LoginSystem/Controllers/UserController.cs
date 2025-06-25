using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LoginSystem.DTOs;
using LoginSystem.Helpers;
using LoginSystem.Services;
using System.IdentityModel.Tokens.Jwt;

namespace LoginSystem.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        private readonly UserService _userService;

        public UserController(JwtHelper jwtHelper, UserService userService)
        {
            _jwtHelper = jwtHelper;
            _userService = userService;
        }

        [HttpGet("infoauth")]
        public IActionResult GetUserInfo()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return Unauthorized("Missing or invalid Authorization header");

            var token = authHeader.Substring("Bearer ".Length);
            var principal = _jwtHelper.GetPrincipalFromToken(token);
            if (principal == null) return Unauthorized("Invalid token");

            var claimsList = principal.Claims.ToList();

            //foreach (var claim in principal.Claims)
            //{
            //    Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
            //}

            var response = new CurrentUserDto
            {
                Username = claimsList.FirstOrDefault(c => c.Type == "username")?.Value ?? "",
                UserId = int.TryParse(claimsList.FirstOrDefault(c => c.Type == "user_ID")?.Value, out var id) ? id : 0,
                Role = claimsList.FirstOrDefault(c => c.Type == "role")?.Value ?? "",
                Permissions = claimsList.Where(c => c.Type == "permission").Select(c => c.Value).ToList(),
                ApplicationCode = claimsList.FirstOrDefault(c => c.Type == "application_code")?.Value ?? "",
                ApplicationName = claimsList.FirstOrDefault(c => c.Type == "application_name")?.Value ?? ""
            };

            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto dto)
        {
            try
            {
                var user = _userService.RegisterUser(dto);
                return Ok(new
                {
                    message = "User registered successfully",
                    user.Id,
                    user.Username,
                    user.RoleId,
                    user.FullName,
                    user.EmployeeCode
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
