namespace towerRedo.Models;

public class Comment : DbItem<int>
{
    public string CreatorId { get; set; }
    public int EventId { get; set; }
    public string Body { get; set; }
    public Boolean IsAttending { get; set; }
    public Account Creator { get; set; }
}