namespace towerRedo.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TicketsController : ControllerBase
  {
    private readonly TicketsService _ticketsService;
    private readonly Auth0Provider _a0;

    public TicketsController(TicketsService ticketsService, Auth0Provider a0)
    {
      _ticketsService = ticketsService;
      _a0 = a0;
    }

    // STUB GET TICKET BY TICKET ID
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Ticket>> GetOne(int id)
    {
      try
      {
        Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
        Ticket ticket = _ticketsService.GetOne(id);
        return Ok(ticket);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // STUB CREATE TICKET
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Ticket>> Create([FromBody] Ticket ticketData)
    {
      try
      {
        Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
        ticketData.AccountId = userInfo.Id;
        Ticket ticket = _ticketsService.Create(ticketData);
        ticket.Creator = userInfo;
        return Ok(ticket);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // STUB DELETE TICKET
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
        string message = _ticketsService.Delete(id, userInfo.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}