namespace towerRedo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly EventsService _eventsService;
    private readonly Auth0Provider _a0;

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

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Remove(int id)
    {
        try
        {
            Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
            string message = _eventsService.Remove(id, userInfo.Id);
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
            Comment comments = _eventsService.GetComments(id);
            return Ok(comments);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
