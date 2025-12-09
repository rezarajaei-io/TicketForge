using AuthService.API.Protos;
using AuthService.Application.Features.Auth.Commands;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Services;

public class AuthGrpcService : AuthGrpc.AuthGrpcBase
{
    private readonly IMediator _mediator;

    public AuthGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        try
        {
            var command = new RegisterUserCommand(request.Username, request.Email, request.Password);
            var userId = await _mediator.Send(command);

            return new RegisterResponse
            {
                Success = true,
                UserId = userId.ToString(),
                Message = "User created successfully"
            };
        }
        catch (Exception ex)
        {
            return new RegisterResponse { Success = false, Message = ex.Message };
        }
    }
}
