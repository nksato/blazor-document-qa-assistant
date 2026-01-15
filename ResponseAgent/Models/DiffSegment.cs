namespace ResponseAgent.Models;

public class DiffSegment
{
    public int Id { get; set; }
    public string Text { get; set; } = "";
    public bool IsAdded { get; set; }
    public string? BeforeText { get; set; }
}
