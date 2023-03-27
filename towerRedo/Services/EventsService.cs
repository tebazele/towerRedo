namespace towerRedo.Services;

public class EventsService
{
  private readonly EventsRepository _repo;

  public EventsService(EventsRepository repo)
  {
    _repo = repo;
  }

  // SECTION EVENT

  // GET ONE
  internal TowerEvent GetOne(int eventId)
  {
    TowerEvent towerEvent = _repo.GetOne(eventId);
    if (towerEvent == null)
    {
      throw new Exception("Event does not exist.");
    }
    return towerEvent;
  }

  // GET ALL
  internal List<TowerEvent> GetAll()
  {
    List<TowerEvent> towerEvents = _repo.GetAll();
    return towerEvents;
  }

  // POST 
  internal TowerEvent Create(TowerEvent eventData)
  {
    TowerEvent towerEvent = _repo.Create(eventData);
    return towerEvent;
  }

  // EDIT 
  internal TowerEvent Edit(int eventId, TowerEvent eventBody)
  {
    TowerEvent originalEvent = this.GetOne(eventId);
    if (eventBody.CreatorId != originalEvent.CreatorId)
    {
      throw new Exception("You do not have permission to edit " + originalEvent.Name + ".");
    }
    originalEvent.Name = eventBody.Name ?? originalEvent.Name;
    originalEvent.Description = eventBody.Description ?? originalEvent.Description;
    originalEvent.CoverImg = eventBody.CoverImg ?? originalEvent.CoverImg;
    originalEvent.IsCancelled = eventBody.IsCancelled ?? originalEvent.IsCancelled;
    originalEvent.Location = eventBody.Location ?? originalEvent.Location;
    originalEvent.Capacity = eventBody.Capacity ?? originalEvent.Capacity;
    originalEvent.StartDate = eventBody.StartDate ?? originalEvent.StartDate;
    originalEvent.Type = eventBody.Type ?? originalEvent.Type;

    bool edited = _repo.Edit(originalEvent);
    if (edited == false)
    {
      throw new Exception("Something went wrong with SQL Tables");
    }
    return originalEvent;
  }

  // DELETE
  // TODO CHANGE THIS TO A CANCELLED EVENT BY ADDING PROPERTY TO EVENT, AND SWITCH TO TRUE, THEN SAVE
  internal String Remove(int eventId, string userId)
  {
    TowerEvent towerEvent = this.GetOne(eventId);
    if (towerEvent.CreatorId == userId)
    {
      throw new Exception("You can't edit " + towerEvent.Name + " event. It was created by someone else.");
    }
    _repo.Remove(eventId);
    return "Event has been deleted";
  }
}

// SECTION COMMENTS

// GET ALL
internal Comment GetComments(int eventId)
{
  throw new NotImplementedException();
}


