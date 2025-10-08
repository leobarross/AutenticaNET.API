namespace AutenticaNET.API.Models
{
    public class User
    {
        public User(string username, string email, string passwordHash)
        {
            Id = Guid.NewGuid().ToString();
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Role = "User";
            CreatedAt = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
