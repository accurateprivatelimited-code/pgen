namespace PGen.Auth;

public sealed class UserAccount
{
    public required string UserName { get; init; }
    public required string PasswordHash { get; init; }
    public required string RoleId { get; set; }
}

