namespace towerRedo.Repositories;

public class CommentsRepository
{
    private readonly IDbConnection _db;

    public CommentsRepository(IDbConnection db)
    {
        _db = db;
    }

    // CREATE
    internal Comment Create(Comment commentData)
    {
        string sql = @"
    INSERT INTO jaComments
    (creatorId, eventId, body, isAttending)
    VALUES
    (@creatorId, @eventId, @body, @isAttending);
    SELECT LAST_INSERT_ID();
    ";
        int id = _db.ExecuteScalar<int>(sql, commentData);
        commentData.Id = id;
        return commentData;
    }

    // DELETE
    internal void Delete(int id)
    {
        string sql = @"
    DELETE FROM jaComments WHERE id = @id; 
    ";
        _db.Execute(sql, new { id });
    }

    // EDIT
    internal Boolean Edit(Comment commentData)
    {
        string sql = @"
    UPDATE jaComments SET
    body = @body,
    WHERE id = @id;
    ";
        int rows = _db.Execute(sql, commentData);
        return rows > 0;
    }

    // GET ONE
    internal Comment GetOne(int commentId)
    {
        string sql = @"
    SELECT
    c.*,
    a.*
    FROM jaComments c
    JOIN accounts a ON c.creatorId = a.id
    WHERE c.id = @commentId;
    ";
        return _db.Query<Comment, Account, Comment>(sql, (c, a) =>
        {
            c.Creator = a;
            return c;
        }, new { commentId }).FirstOrDefault();
    }
}
