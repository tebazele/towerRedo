namespace towerRedo.Repositories;

public class EventsRepository
{
  private readonly IDbConnection _db;

  public EventsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal TowerEvent Create(TowerEvent eventData)
  {
    string sql = @"
        INSERT INTO jaEvents
        (name, description, coverImg, location, capacity, startDate, type, creatorId)
        VALUES 
        (@name, @description, @coverImg, @location, @capacity, @startDate, @type, @creatorId);
        SELECT LAST_INSERT_ID();
        ";

    int id = _db.ExecuteScalar<int>(sql, eventData);
    eventData.Id = id;
    return eventData;
  }

  internal bool Edit(TowerEvent originalEvent)
  {
    string sql = @"
        UPDATE jaEvents SET
        name = @name,
        description = @description,
        coverImg = @coverImg,
        isCancelled = @isCancelled,
        location = @location,
        capacity = @capacity,
        startDate = @startDate,
        type = @type
        WHERE id = @id;
        ";
    int rows = _db.Execute(sql, originalEvent);
    return rows > 0;
  }

  internal List<TowerEvent> GetAll()
  {
    string sql = @"
        SELECT
        e.*,
        a.*
        FROM jaEvents e
        JOIN accounts a ON e.creatorId = a.id;
        ";
    return _db.Query<TowerEvent, Account, TowerEvent>(sql, (e, p) =>
    {
      e.Creator = p;
      return e;

    }).ToList();
  }

  internal TowerEvent GetOne(int id)
  {
    string sql = @"
        SELECT
        e.*,
        a.*
        FROM jaEvents e
        JOIN accounts a ON e.creatorId = a.id
        WHERE e.id = @id;
        ";
    return _db.Query<TowerEvent, Account, TowerEvent>(sql, (e, a) =>
    {
      e.Creator = a;
      return e;
    }, new { id }).FirstOrDefault();
  }

  internal void Remove(int eventId)
  {
    string sql = @"
        DELETE FROM 
        jaEvents
        WHERE id = @eventId;
        ";

    _db.Execute(sql, new { eventId });
  }
}
