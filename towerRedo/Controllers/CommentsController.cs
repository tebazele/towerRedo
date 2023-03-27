namespace towerRedo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{

  private readonly CommentsService _commentsService;
  private readonly Auth0Provider _a0;

  public CommentsController(CommentsService commentsService, Auth0Provider a0)
  {
    _commentsService = commentsService;
    _a0 = a0;
  }

  // CREATE
  [HttpPost]
  [Authorize]

  // EDIT -- EXTRA
  [HttpPut("{id}")]
  [Authorize]

  // DELETE
  [HttpDelete("{id}")]
  [Authorize]

}
