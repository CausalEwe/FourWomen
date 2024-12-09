using MediatR;

namespace AuthorizationService.Application.Commands;

public sealed record UserRegisterCommand : IRequest<UserRegisterResult>
{
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string Email { get; init; }
}

public sealed record UserRegisterResult
{
    public required bool Success { get; init; }
}

public class UserRegisterCommandHandler() : IRequestHandler<UserRegisterCommand, UserRegisterResult>
{
    public async Task<UserRegisterResult> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        return new UserRegisterResult { Success = true };
    }
}