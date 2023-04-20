namespace towerRedo.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    private readonly Auth0Provider _auth0Provider;
    private readonly TicketsService _ticketsService;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider, TicketsService ticketsService)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _ticketsService = ticketsService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            return Ok(_accountService.GetOrCreateProfile(userInfo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpPut]
    [Authorize]
    public async Task<ActionResult<Account>> Edit([FromBody] Account accountBody)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Account editedAccount = _accountService.Edit(accountBody, userInfo.Email);
            return Ok(editedAccount);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // SECTION TICKETS

    [HttpGet("tickets")]
    [Authorize]
    public async Task<ActionResult<List<TicketEvent>>> GetTickets()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<TicketEvent> tickets = _ticketsService.GetByAccountId(userInfo.Id);
            return Ok(tickets);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("events")]
    [Authorize]

    public async Task<ActionResult<List<TowerEvent>>> GetMyEvents()
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<TowerEvent> events = _accountService.GetMyEvents(userInfo.Id);

            return Ok(events);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // SECTION EVENTS
}
