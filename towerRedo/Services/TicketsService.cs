namespace towerRedo.Services
{
  public class TicketsService
  {

    private readonly TicketsRepository _repo;
    private readonly EventsService _events;

    public TicketsService(TicketsRepository repo, EventsService events)
    {
      _repo = repo;
      _events = events;
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
      Ticket ticket = _repo.Create(ticketData);
      TowerEvent towerEvent = _events.GetOne(ticketData.EventId);
      if (towerEvent.Capacity > 0)
      {
        towerEvent.Capacity++;
        _events.Edit(towerEvent.Id, towerEvent);
      }
      else
      {
        throw new Exception(towerEvent.Name + " is Sold Out!");
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
}
