namespace StajTakipSistemi.Models;

public class WorkHistory
{
    public Guid Id { get; set; }
    public string Company { get; set; }
    public string Description { get; set; }

    public Guid HistoryId { get; set; }
    public History History { get; set; }
}