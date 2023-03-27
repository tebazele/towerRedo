namespace towerRedo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
  private readonly EventsService _eventsService;
  private readonly Auth0Provider _a0;
  private readonly TicketsService _tickets;

  public EventsController(EventsService eventsService, Auth0Provider auth0Provider)
  {
    _eventsService = eventsService;
    _a0 = auth0Provider;
  }

  [HttpPost]
  [Authorize]

  public async Task<ActionResult<TowerEvent>> Create([FromBody] TowerEvent eventData)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      eventData.CreatorId = userInfo.Id;
      TowerEvent towerEvent = _eventsService.Create(eventData);
      towerEvent.Creator = userInfo;
      return Ok(towerEvent);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // GET ALL
  [HttpGet]
  public ActionResult<List<TowerEvent>> GetAll()
  {
    try
    {
      List<TowerEvent> towerEvents = _eventsService.GetAll();
      return Ok(towerEvents);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // GET ONE
  [HttpGet("{id}")]
  public ActionResult<TowerEvent> GetOne(int id)
  {
    try
    {
      TowerEvent towerEvent = _eventsService.GetOne(id);
      return Ok(towerEvent);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // EDIT
  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<TowerEvent>> Edit(int id, [FromBody] TowerEvent eventData)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      eventData.CreatorId = userInfo.Id;
      TowerEvent updatedEvent = _eventsService.Edit(id, eventData);
      return Ok(updatedEvent);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // DELETE
  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<String>> Cancel(int id)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      string message = _eventsService.Cancel(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // SECTION COMMENT

  [HttpGet("{id}/comments")]
  public ActionResult<List<Comment>> GetComments(int id)
  {
    try
    {
      List<Comment> comments = _eventsService.GetComments(id);
      return Ok(comments);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // SECTION TICKETS

  [HttpGet("{id}/tickets")]
  public ActionResult<List<Ticket>> GetTickets(int id)
  {
    try
    {
      List<Ticket> tickets = _tickets.GetTickets(id);
      return Ok(tickets);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // SECTION STRETCH GOALS

  // TODO MAYBE COME BACK AND MAKE SEARCH BAR MORE ELABORATE
  // GET SEARCH QUERY
  [HttpGet]
  public ActionResult<List<TowerEvent>> GetByQuery(string query)
  {
    try
    {
      List<TowerEvent> towerEvent = _eventsService.GetByQuery(query);
      return Ok();
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}