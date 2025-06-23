namespace LoginSystem.DTOs
{
    public class CurrentUserDto
    {
        public string Username { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public List<string> Permissions { get; set; }
        public string ApplicationCode { get; set; }
        public string ApplicationName { get; set; }
    }
}
