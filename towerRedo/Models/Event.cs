namespace towerRedo.Models;
using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Type
{
    Concert, Convention, Sport, Digital, Other
}
public class TowerEvent : DbItem<int>
{
    public String Name { get; set; }
    public String Description { get; set; }
    public String CoverImg { get; set; }
    public String Location { get; set; }
    public int? Capacity { get; set; }
    public Boolean? IsCanceled { get; set; }
    public String StartDate { get; set; }
    public Type? Type { get; set; }
    public Account Creator { get; set; }
    public String CreatorId { get; set; }
}

public class TicketEvent : TowerEvent
{
    public int TicketId { get; set; }
    public string AccountId { get; set; }
}