using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;

namespace AuthService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();

    public Task AddAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        var user = _users.FirstOrDefault(u => u.Username == username);
        return Task.FromResult(user);
    }
}