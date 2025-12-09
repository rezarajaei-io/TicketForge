using MediatR;

namespace AuthService.Application.Features.Auth.Commands;

    public record struct RegisterUserCommand(string Username, string Email, string Password) : IRequest<Guid>;
