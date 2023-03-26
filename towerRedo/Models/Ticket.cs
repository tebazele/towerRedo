namespace towerRedo.Models;

public class Ticket : DbItem<int>
{
    public int EventId { get; set; }
    public string accountId { get; set; }
}