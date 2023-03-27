namespace towerRedo.Repositories;

public class EventsRepository
{
    private readonly IDbConnection _db;

    public EventsRepository(IDbConnection db)
    {
        _db = db;
    }

<<<<<<< HEAD
  // CREATE
  internal TowerEvent Create(TowerEvent eventData)
  {
    string sql = @"
=======
    internal TowerEvent Create(TowerEvent eventData)
    {
        string sql = @"
>>>>>>> 5b005629af0d648dd5b60e94255cd59910d08a8a
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

<<<<<<< HEAD
  // EDIT
  internal bool Edit(TowerEvent originalEvent)
  {
    string sql = @"
=======
    internal bool Edit(TowerEvent originalEvent)
    {
        string sql = @"
>>>>>>> 5b005629af0d648dd5b60e94255cd59910d08a8a
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

<<<<<<< HEAD
  // GET ALL
  internal List<TowerEvent> GetAll()
  {
    string sql = @"
=======
    internal List<TowerEvent> GetAll()
    {
        string sql = @"
>>>>>>> 5b005629af0d648dd5b60e94255cd59910d08a8a
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
<<<<<<< HEAD
      e.Creator = p;
      return e;

    }).ToList();
  }

  // GET ONE
  internal TowerEvent GetOne(int id)
  {
    string sql = @"
=======
        string sql = @"
>>>>>>> 5b005629af0d648dd5b60e94255cd59910d08a8a
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

<<<<<<< HEAD
  // DELETE
  internal void Remove(int eventId)
  {
    string sql = @"
=======
    internal void Remove(int eventId)
    {
        string sql = @"
>>>>>>> 5b005629af0d648dd5b60e94255cd59910d08a8a
        DELETE FROM 
        jaEvents
        WHERE id = @eventId;
        ";

        _db.Execute(sql, new { eventId });
    }

    // SECTION COMMENTS

<<<<<<< HEAD
  // GET COMMENTS BY EVENT ID
  internal List<Comment> GetComments(int eventId)
  {
    string sql = @"
=======
    internal List<Comment> GetComments(int eventId)
    {
        string sql = @"
>>>>>>> 5b005629af0d648dd5b60e94255cd59910d08a8a
    SELECT
    c.*,
    a.*
    FROM jaComments c
    JOIN accounts a ON c.creatorId = a.id
    WHERE c.eventId = @eventId;
    ";
        return _db.Query<Comment, Account, Comment>(sql, (c, a) =>
        {
            c.Creator = a;
            return c;
        }, new { eventId }).ToList();
    }

}
