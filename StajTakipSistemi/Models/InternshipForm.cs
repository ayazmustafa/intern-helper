namespace StajTakipSistemi.Models;

public class InternshipForm
{
    public Guid Id { get; set; }
    public string Company { get; set; }
    public string Description { get; set; }
    public bool IsApproved { get; set; }
    public bool IsSucessfullyFinished { get; set; }
    
    public Guid HistoryId { get; set; }
    public History History { get; set; }
}