namespace AuthorizationService.Domain;

public sealed record UserRegisterRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}