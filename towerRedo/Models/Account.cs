namespace towerRedo.Models;

public class Account : DbItem<string>
{
  public string Name { get; set; }
  public string Email { get; set; }
  public string Picture { get; set; }
}
