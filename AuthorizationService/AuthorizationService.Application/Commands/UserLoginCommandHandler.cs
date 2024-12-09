using MediatR;

namespace AuthorizationService.Application.Commands;

public sealed record UserLoginCommand : IRequest<UserLoginResult>
{
    public required string Username { get; init; }
    
    public required string Password { get; init; }
}

public sealed record UserLoginResult
{
    public required string Token { get; init; }
}

public class UserLoginCommandHandler() : IRequestHandler<UserLoginCommand, UserLoginResult>
{
    public async Task<UserLoginResult> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        
        return new UserLoginResult { Token = request.Password };
    }
}
