namespace StajTakipSistemi.Models;

public class History
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<WorkHistory> WorkHistories { get; set; }
    public ICollection<InternshipForm> InternshipForms { get; set; }
}

