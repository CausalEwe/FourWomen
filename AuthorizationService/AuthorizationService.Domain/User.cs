namespace AuthorizationService.Domain;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    
    public string Username { get; private set; } = string.Empty;
    
    public string Password { get; private set; } = string.Empty;

    public bool IsConfirmed { get; private set; } = false; // подтверждена регистрация

    private User()
    {
    }

    public User(Guid id, string username, string password, bool isConfirmed)
    {
        Id = id;
        Username = username;
        Password = password;
        IsConfirmed = isConfirmed;
    }

    public User SetConfirmed()
    {
        IsConfirmed = true;
        return this;
    }
}