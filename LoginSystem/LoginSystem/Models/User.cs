using System.ComponentModel.DataAnnotations.Schema;

namespace LoginSystem.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("employee_code")]
        public string EmployeeCode { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
