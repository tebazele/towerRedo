namespace towerRedo.Repositories
{
  public class TicketsRepository
  {
    private readonly IDbConnection _db;

    public TicketsRepository(IDbConnection db)
    {
      _db = db;
    }
  }
}