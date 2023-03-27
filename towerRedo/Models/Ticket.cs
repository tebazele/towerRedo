namespace towerRedo.Models;

public class Ticket : DbItem<int>
{
  public int EventId { get; set; }
  public string AccountId { get; set; }
  public Account Creator { get; set; }
}