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

    // STUB CREATE TICKET
    [HttpPost]
    [Authorize]

    // STUB DELETE TICKET
    [HttpDelete("{id}")]
    [Authorize]
  }
}