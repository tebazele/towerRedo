namespace towerRedo.Services;

public class CommentsService
{

  private readonly CommentsRepository _repo;

  public CommentsService(CommentsRepository repo)
  {
    _repo = repo;
  }

  internal Comment Create(Comment commentData)
  {
    Comment comment = _repo.Create(commentData);
    return comment;
  }

  internal Comment GetOne(int commentId)
  {
    Comment comment = _repo.GetOne(commentId);
    if (comment == null)
    {
      throw new Exception("That comment doesn't exist.");
    }
    return comment;
  }

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
