namespace StajTakipSistemi.Models;

public class Role
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; }
}