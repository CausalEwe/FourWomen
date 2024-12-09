using MediatR;

namespace AuthorizationService.Application.Commands;

public sealed record UserConfirmCommand : IRequest<UserConfirmResult>
{
    public required string ConfirmationToken { get; init; }
}

public sealed record UserConfirmResult
{
    public required bool Success { get; init; }
}

public class UserConfirmCommandHandler() : IRequestHandler<UserConfirmCommand, UserConfirmResult>
{
    public async Task<UserConfirmResult> Handle(UserConfirmCommand request, CancellationToken cancellationToken)
    {
        return new UserConfirmResult { Success = true };
    }
}