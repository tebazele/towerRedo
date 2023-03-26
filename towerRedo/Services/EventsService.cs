namespace towerRedo.Services;

public class EventsService
{
    private readonly EventsRepository _repo;

    public EventsService(EventsRepository repo)
    {
        _repo = repo;
    }

    internal TowerEvent Create(TowerEvent eventData)
    {
        TowerEvent towerEvent = _repo.Create(eventData);
        return towerEvent;
    }

    internal TowerEvent Edit(int id, TowerEvent eventBody)
    {
        TowerEvent originalEvent = this.GetOne(id);
        if (eventBody.CreatorId != originalEvent.CreatorId)
        {
            throw new Exception("You do not have permission to edit " + originalEvent.Name + ".");
        }
        originalEvent.Name = eventBody.Name ?? originalEvent.Name;
        originalEvent.Description = eventBody.Description ?? originalEvent.Description;
        originalEvent.CoverImg = eventBody.CoverImg ?? originalEvent.CoverImg;
        originalEvent.Location = eventBody.Location ?? originalEvent.Location;
        originalEvent.Capacity = eventBody.Capacity ?? originalEvent.Capacity;
        originalEvent.StartDate = eventBody.StartDate ?? originalEvent.StartDate;
        originalEvent.Type = eventBody.Type ?? originalEvent.Type;

        bool edited = _repo.Edit(originalEvent);
        return originalEvent;
    }

    internal List<TowerEvent> GetAll()
    {
        List<TowerEvent> towerEvents = _repo.GetAll();
        return towerEvents;
    }

    internal Comment GetComments(int id)
    {
        throw new NotImplementedException();
    }

    internal TowerEvent GetOne(int id)
    {
        TowerEvent towerEvent = _repo.GetOne(id);
        if (towerEvent == null)
        {
            throw new Exception("Event does not exist.");
        }
        return towerEvent;
    }

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
