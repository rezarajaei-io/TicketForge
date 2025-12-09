using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        //check Repeated
        var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
        if (existingUser != null)
        {
            throw new Exception("Username already exists.");
        }

        // 2. Password Hash
        var passwordHash = _passwordHasher.HashPassword(request.Password);

        // 3. Create User Factory
        var newUser = User.Create(request.Username, request.Email, passwordHash);

        // 4. Add To Repository
        await _userRepository.AddAsync(newUser);

        return newUser.Id;
    }
}
