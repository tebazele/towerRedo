namespace towerRedo.Repositories;

public class EventsRepository
{
  private readonly IDbConnection _db;

  public EventsRepository(IDbConnection db)
  {
    _db = db;
  }

  // CREATE
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

  // EDIT
  internal bool Edit(TowerEvent originalEvent)
  {
    string sql = @"
        UPDATE jaEvents SET
        name = @name,
        description = @description,
        coverImg = @coverImg,
        isCanceled = @isCanceled,
        location = @location,
        capacity = @capacity,
        startDate = @startDate,
        type = @type
        WHERE id = @id;
        ";
    int rows = _db.Execute(sql, originalEvent);
    return rows > 0;
  }

  // GET ALL
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

  // GET ONE
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

  // DELETE
  internal void Remove(int eventId)
  {
    string sql = @"
        DELETE FROM 
        jaEvents
        WHERE id = @eventId;
        ";

    _db.Execute(sql, new { eventId });
  }

  // SECTION COMMENTS



  internal List<TowerEvent> GetByQuery(string query)
  {
    string searchTerm = $"%{query}%";
    string sql = @"
    SELECT
    e.*,
    a.*
    FROM jaEvents e
    JOIN accounts a ON e.creatorId = a.id
    WHERE e.type LIKE @searchTerm OR
    e.name LIKE @searchTerm OR
    e.description LIKE @searchTerm OR
    e.location LIKE @searchTerm;
    ";
    return _db.Query<TowerEvent, Account, TowerEvent>(sql, (e, a) =>
    {
      e.Creator = a;
      return e;
    }, new { searchTerm }).ToList();
  }
}
