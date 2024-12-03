using StajTakipSistemi.Authentication.PasswordHasher;

namespace StajTakipSistemi.Models;

public class User 
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set;  } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    
    public History History { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }

    public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, Password);
    }

}