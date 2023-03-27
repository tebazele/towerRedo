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
  public async Task<ActionResult<Comment>> Create([FromBody] Comment commentData)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      commentData.CreatorId = userInfo.Id;
      Comment comment = _commentsService.Create(commentData);
      return Ok(comment);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // EDIT -- EXTRA
  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Comment>> Edit([FromBody] Comment commentData, int id)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      commentData.CreatorId = userInfo.Id;
      Comment editedComment = _commentsService.Edit(commentData, id);
      return Ok(editedComment);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  // DELETE
  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> Delete(int id)
  {
    try
    {
      Account userInfo = await _a0.GetUserInfoAsync<Account>(HttpContext);
      string message = _commentsService.Delete(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
