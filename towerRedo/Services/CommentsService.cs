namespace towerRedo.Services;

public class CommentsService
{

  private readonly CommentsRepository _repo;
  private readonly TicketsService _tickets;
  private readonly EventsRepository _events;

  public CommentsService(CommentsRepository repo, TicketsService tickets, EventsRepository events)
  {
    _repo = repo;
    _tickets = tickets;
    _events = events;
  }

  // CREATE
  internal Comment Create(Comment commentData)
  {
    List<Ticket> tickets = _tickets.GetByEventId(commentData.EventId);
    foreach (Ticket te in tickets)
    {
      if (te.AccountId == commentData.CreatorId)
      {
        commentData.IsAttending = true;
      }
    }
    Comment createdComment = _repo.Create(commentData);
    Comment comment = _repo.GetOne(createdComment.Id);
    return comment;
  }

  // GET ALL
  internal List<Comment> GetComments(int eventId)
  {
    List<Comment> comments = _repo.GetComments(eventId);
    return comments;
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
    comment.IsAttending = commentData.IsAttending ?? comment.IsAttending;

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
