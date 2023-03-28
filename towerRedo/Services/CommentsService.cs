namespace towerRedo.Services;

public class CommentsService
{

  private readonly CommentsRepository _repo;
  private readonly TicketsService _tickets;

  public CommentsService(CommentsRepository repo, TicketsService tickets)
  {
    _repo = repo;
    _tickets = tickets;
  }

  // CREATE
  internal Comment Create(Comment commentData)
  {
    List<TicketEvent> tickets = _tickets.GetByAccountId(commentData.CreatorId);
    foreach (TicketEvent te in tickets)
    {
      if (te.CreatorId == commentData.CreatorId)
      {
        commentData.IsAttending = true;
      }
    }
    Comment comment = _repo.Create(commentData);
    return comment;
  }

  // GET ONE
  internal Comment GetOne(int commentId)
  {
    Comment comment = _repo.GetOne(commentId);
    if (comment == null)
    {
      throw new Exception("That comment doesn't exist.");
    }
    return comment;
  }

  // EDIT
  internal Comment Edit(Comment commentData, int commentId)
  {
    Comment comment = this.GetOne(commentId);
    if (commentData.CreatorId != comment.CreatorId)
    {
      throw new Exception("You do not have permission to edit this comment.");
    }
    comment.Body = commentData.Body ?? comment.Body;

    Boolean isEdited = _repo.Edit(comment);
    if (isEdited == false)
    {
      throw new Exception("Edited did not go through repository correctly.");
    }
    return comment;
  }

  // DELETE
  internal string Delete(int commentId, string userId)
  {
    Comment comment = this.GetOne(commentId);
    if (comment.CreatorId != userId)
    {
      throw new Exception("You do not have permission to delete this comment.");
    }
    _repo.Delete(comment.Id);
    return "Comment has been successfully deleted";
  }
}
