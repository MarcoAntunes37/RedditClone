namespace RedditClone.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class CommentRepository : ICommentRepository
{
    private readonly RedditCloneDbContext _dbContext;

    public CommentRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Comment comment)
    {
        _dbContext.Comments.Add(comment);

        _dbContext.SaveChangesAsync();
    }

    public void UpdateCommentById(CommentId id, UserId userId, string content)
    {
        Comment comment = _dbContext.Comments.SingleOrDefault(c => c.Id == id && c.UserId == userId)
            ?? throw new Exception("An error occurred, comment is invalid or you not the owner");

        comment.UpdateComment(content);

        _dbContext.Comments.Update(comment);

        _dbContext.Entry(comment).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void DeleteCommentById(CommentId id, UserId userId)
    {
        Comment comment = _dbContext.Comments.SingleOrDefault(c => c.Id == id && c.UserId == userId)
            ?? throw new Exception("An error occurred, comment is invalid or you not the owner");

        _dbContext.Comments.Remove(comment);

        _dbContext.SaveChanges();
    }

    public void AddCommentVote(CommentId id, UserId userId, bool isVoted)
    {
        Comment commentVote = _dbContext.Comments
            .Include(p => p.Votes)
            .SingleOrDefault(p => p.Id == id)
            ?? throw new Exception("An error occurred, comment is invalid.");

        var vote = Votes.Create(
            id,
            userId,
            isVoted);

        commentVote.AddVote(vote);

        _dbContext.Comments.Update(commentVote);

        _dbContext.Entry(commentVote).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void UpdateCommentVoteById(CommentId id, VoteId voteId, UserId userId, bool isVoted)
    {
        Comment commentVote = _dbContext.Comments
            .Include(p => p.Votes)
            .Where(p => p.Votes.Any(pv => pv.Id == voteId && pv.UserId == userId))
            .SingleOrDefault(p => p.Id == id)
            ?? throw new Exception("An error occurred, comment is invalid or vote in comment is invalid");

        commentVote.UpdateVote(voteId, isVoted);

        _dbContext.Entry(commentVote).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void DeleteCommentVoteById(CommentId id, VoteId voteId, UserId userId)
    {
        Comment commentVote = _dbContext.Comments
            .Include(p => p.Votes)
            .Where(p => p.Votes.Any(pv => pv.Id == voteId && pv.UserId == userId))
            .SingleOrDefault(p => p.Id == id)
            ?? throw new Exception("An error occurred, comment is invalid or vote in comment is invalid");

        commentVote.RemoveVote(voteId);

        _dbContext.Entry(commentVote).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void AddCommentReply(CommentId id, UserId userId, string content)
    {
        Comment commentReply = _dbContext.Comments
            .Include(p => p.Replies)
            .SingleOrDefault(p => p.Id == id)
            ?? throw new Exception("An error occurred, comment is invalid.");

        List<RepliesVotes> repliesVotes = new();

        var reply = Replies.Create(
            userId,
            id,
            content,
            DateTime.UtcNow,
            DateTime.UtcNow,
            repliesVotes);

        commentReply.AddReply(reply);

        _dbContext.Comments.Update(commentReply);

        _dbContext.Entry(commentReply).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void UpdateCommentReplyById(CommentId id, ReplyId replyId, UserId userId, string content)
    {
        Comment commentReply = _dbContext.Comments
            .Include(p => p.Replies)
            .Where(p => p.Replies.Any(pv => pv.Id == replyId && pv.UserId == userId))
            .SingleOrDefault(p => p.Id == id)
            ?? throw new Exception("An error occurred, comment is invalid or vote in comment is invalid");

        List<RepliesVotes> repliesVotes = new();

        commentReply.UpdateReply(replyId, content);

        _dbContext.Entry(commentReply).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void DeleteCommentReplyById(CommentId id, ReplyId replyId, UserId userId)
    {
        Comment commentReply = _dbContext.Comments
            .Include(p => p.Replies)
            .Where(p => p.Replies.Any(pv => pv.Id == replyId && pv.UserId == userId))
            .SingleOrDefault(p => p.Id == id)
            ?? throw new Exception("An error occurred, comment is invalid or vote in comment is invalid");

        commentReply.RemoveReply(replyId);

        _dbContext.Entry(commentReply).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }
}