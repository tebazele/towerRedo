namespace towerRedo.Repositories
{
  public class TicketsRepository
  {
    private readonly IDbConnection _db;

    public TicketsRepository(IDbConnection db)
    {
      _db = db;
    }

    // CREATE
    internal Ticket Create(Ticket ticketData)
    {
      string sql = @"
      INSERT INTO jaTickets
      (eventId, accountId)
      VALUES
      (@eventId, @accountId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, ticketData);
      ticketData.Id = id;
      return ticketData;
    }

    // DELETE
    internal Boolean Delete(int id)
    {
      string sql = @"
      DELETE FROM jaTickets WHERE id = @id;
      ";
      int rows = _db.Execute(sql, new { id });
      return rows > 0;
    }

    // GET BY TICKET ID
    internal Ticket GetOne(int id)
    {
      string sql = @"
      SELECT
      t.*,
      a.*
      FROM jaTickets t
      JOIN accounts a ON t.accountId = a.id
      WHERE t.id = @id;
      ";
      return _db.Query<Ticket, Account, Ticket>(sql, (t, a) =>
      {
        t.Creator = a;
        return t;
      }, new { id }).FirstOrDefault();
    }

    // GET BY EVENT ID
    internal List<Ticket> GetByEventId(int eventId)
    {
      string sql = @"
      SELECT
      t.*,
      a.*
      FROM jaTickets t
      JOIN accounts a ON t.accountId = a.id
      WHERE t.eventId = @eventId;
      ";
      return _db.Query<Ticket, Account, Ticket>(sql, (t, a) =>
      {
        t.Creator = a;
        return t;
      }, new { eventId }).ToList();
    }

    // GET BY ACCOUNT ID
    internal List<TicketEvent> GetByAccountId(string accountId)
    {
      string sql = @"
      SELECT
      e.*,
      a.*,
      t.*
      FROM jaEvents e
      JOIN accounts a ON e.creatorId = a.id
      JOIN jaTickets t ON t.eventId = e.id
      WHERE t.accountId = @accountId;
      ";
      return _db.Query<TicketEvent, Account, Ticket, TicketEvent>(sql, (te, a, t) =>
      {
        te.Creator = a;
        te.TicketId = t.Id;
        te.AccountId = t.AccountId;

        return te;
      }, new { accountId }).ToList();
    }
  }
}