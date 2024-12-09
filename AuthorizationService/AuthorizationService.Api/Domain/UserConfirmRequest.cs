namespace AuthorizationService.Domain;

public sealed record UserConfirmRequest
{
    public required string ConfirmationToken { get; set; }
}