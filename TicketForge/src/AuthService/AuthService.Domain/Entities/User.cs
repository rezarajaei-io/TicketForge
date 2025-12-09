namespace AuthService.Domain.Entities;

    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Constructor خصوصی برای اینکه فقط از طریق Factory ساخته شود
        private User(string username, string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
        }

        // Factory Method pattern
        public static User Create(string username, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty");

            // TODO : Add Validations
            return new User(username, email, passwordHash);
        }
    }
