namespace PGen.Auth;

[Flags]
public enum UserRight
{
    None = 0,
    
    // User Management Rights
    ViewUsers = 1 << 0,
    CreateUsers = 1 << 1,
    EditUsers = 1 << 2,
    DeleteUsers = 1 << 3,
    
    // Role Management Rights
    ViewRoles = 1 << 4,
    CreateRoles = 1 << 5,
    EditRoles = 1 << 6,
    DeleteRoles = 1 << 7,
    
    // Password Generation Rights
    GeneratePasswords = 1 << 8,
    
    // License Management Rights
    ManageLicenses = 1 << 9,
    
    // Settings Rights
    ViewSettings = 1 << 10,
    EditSettings = 1 << 11,
    
    // All rights
    All = (1 << 12) - 1
}
