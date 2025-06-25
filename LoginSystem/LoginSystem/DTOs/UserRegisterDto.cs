namespace LoginSystem.DTOs
{
    public class UserRegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string EmployeeCode { get; set; }
    }
}
