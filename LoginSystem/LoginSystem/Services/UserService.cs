using LoginSystem.Data;
using LoginSystem.DTOs;
using LoginSystem.Models;

namespace LoginSystem.Services
{
    public class UserService
    {
        private readonly SSODbContext _context;

        public UserService(SSODbContext context)
        {
            _context = context;
        }

        public User RegisterUser(UserRegisterDto dto)
        {
            // Kiểm tra trùng username
            if (_context.Users.Any(u => u.Username == dto.Username))
                throw new Exception("Username already exists");

            // Kiểm tra trùng employee_code
            if (_context.Users.Any(u => u.EmployeeCode == dto.EmployeeCode))
                throw new Exception("Employee code already exists");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = dto.Password, // Mã hoá sauuu
                RoleId = dto.RoleId,
                FullName = dto.FullName,
                EmployeeCode = dto.EmployeeCode,
                IsActive = true
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}
