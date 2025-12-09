using AuthService.Domain.Interfaces;

namespace AuthService.Infrastructure.Services;

public class BcryptPasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyPassword(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}