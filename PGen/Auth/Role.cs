namespace PGen.Auth;

public sealed class Role
{
    public required string Id { get; init; }
    public required string Name { get; set; }
    public required UserRight Rights { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedUtc { get; init; }

    public bool HasRight(UserRight right)
    {
        return (Rights & right) == right;
    }

    public void GrantRight(UserRight right)
    {
        Rights |= right;
    }

    public void RevokeRight(UserRight right)
    {
        Rights &= ~right;
    }

    public override string ToString() => Name;
}
