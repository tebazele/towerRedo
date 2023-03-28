namespace towerRedo.Services
{
  public class TicketsService
  {

    private readonly TicketsRepository _repo;
    private readonly EventsService _events;
    private readonly CommentsRepository _commentsRepo;

    public TicketsService(TicketsRepository repo, EventsService events, CommentsRepository commentsRepo)
    {
      _repo = repo;
      _events = events;
      _commentsRepo = commentsRepo;
    }

    // TODO GET TICKET BY EVENT ID

    internal List<Ticket> GetByEventId(int eventId)
    {
      List<Ticket> tickets = _repo.GetByEventId(eventId);
      return tickets;
    }

    // TODO GET TICKETS BY ACCOUNT ID

    // CREATE
    internal Ticket Create(Ticket ticketData)
    {

      TowerEvent towerEvent = _events.GetOne(ticketData.EventId);
      List<TicketEvent> accountTicket = this.GetByAccountId(ticketData.AccountId);
      foreach (TicketEvent te in accountTicket)
      {
        if (te.Id == towerEvent.Id)
        {
          throw new Exception("You already have a ticket to this event.");
        }
      }
      if (towerEvent.Capacity > 0)
      {
        towerEvent.Capacity--;
        _events.Edit(towerEvent.Id, towerEvent);
      }
      else
      {
        throw new Exception(towerEvent.Name + " is Sold Out!");
      }
      Ticket ticket = _repo.Create(ticketData);
      List<Comment> comments = _commentsRepo.GetByEventId(ticket.EventId, ticket.AccountId);

      // SECTION foreach edit isAttending on these comments and call commentsRepo.edit()
      foreach (Comment c in comments)
      {
        c.IsAttending = true;
        _commentsRepo.Edit(c);
      }


      return ticket;
    }

    // DELETE
    internal string Delete(int ticketId, string userId)
    {
      Ticket ticket = this.GetOne(ticketId);
      if (ticket.AccountId != userId)
      {
        throw new Exception("You do not have permission to delete this ticket!");
      }
      Boolean isDeleted = _repo.Delete(ticket.Id);
      if (isDeleted == false)
      {
        throw new Exception("Something went wrong in the repo dawg.");
      }

      TowerEvent towerEvent = _events.GetOne(ticket.EventId);
      towerEvent.Capacity++;
      _events.Edit(towerEvent.Id, towerEvent);

      return ("Your ticket for " + towerEvent.Name + " was deleted.");
    }

    // GET BY TICKET ID
    internal Ticket GetOne(int ticketId)
    {
      Ticket ticket = _repo.GetOne(ticketId);
      if (ticket == null)
      {
        throw new Exception("Ticket = Null");
      }
      return ticket;
    }


    // GET BY ACCOUNT ID
    internal List<TicketEvent> GetByAccountId(string accountId)
    {
      List<TicketEvent> tickets = _repo.GetByAccountId(accountId);
      return tickets;
    }

    // GET TICKETS
    internal List<Ticket> GetTickets(int eventId)
    {
      List<Ticket> tickets = _repo.GetByEventId(eventId);
      return tickets;
    }
  }
      if (towerEvent.Capacity > 0)
      {
        towerEvent.Capacity++;
        _events.Edit(towerEvent.Id, towerEvent);
      }
      else
{
  throw new Exception(towerEvent.Name + " is Sold Out!");
}
Ticket ticket = _repo.Create(ticketData);
List<Comment> comment = _commentsRepo.GetByEventId(ticket.EventId, ticket.AccountId);
return ticket;
    }
}
