namespace RedditClone.Infrastructure.Persistence;

using System.Net;
using Microsoft.EntityFrameworkCore;
using RedditClone.Application.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class CommentRepository : ICommentRepository
{
    private readonly RedditCloneDbContext _dbContext;

    public CommentRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Comment GetCommentById(CommentId commentId)
    {
        Comment comment = _dbContext.Comments.FirstOrDefault(c => c.Id == commentId)
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Comment not found");

        return comment;
    }

    public List<Comment> GetCommentsListByPostId(PostId postId)
    {
        List<Comment> comments =
            _dbContext.Comments.Where(c => c.PostId == postId).ToList();

        return comments;
    }

    public void Add(Comment comment)
    {
        _dbContext.Comments.Add(comment);

        _dbContext.SaveChangesAsync();
    }

    public void UpdateCommentById(CommentId id, UserId userId, string content)
    {
        Comment comment = _dbContext.Comments.SingleOrDefault(c => c.Id == id && c.UserId == userId)
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Comment not found on you comments");

        comment.UpdateComment(content);

        _dbContext.Comments.Update(comment);

        _dbContext.Entry(comment).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void DeleteCommentById(CommentId id, UserId userId)
    {
        Comment comment = _dbContext.Comments.SingleOrDefault(c => c.Id == id && c.UserId == userId)
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Comment not found on you comments");

        _dbContext.Comments.Remove(comment);

        _dbContext.SaveChanges();
    }

    public void AddCommentVote(CommentId id, UserId userId, bool isVoted)
    {
        Comment commentVote = _dbContext.Comments
            .Include(p => p.Votes)
            .SingleOrDefault(p => p.Id == id)
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Comment not found");

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
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Your vote not found on comment");

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
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Your vote not found on comment");

        commentVote.RemoveVote(voteId);

        _dbContext.Entry(commentVote).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void AddCommentReply(CommentId id, UserId userId, CommunityId communityId, string content)
    {
        Comment commentReply = _dbContext.Comments
            .Include(p => p.Replies)
            .SingleOrDefault(p => p.Id == id)
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Reply not found on comment");

        List<RepliesVotes> repliesVotes = new();

        var reply = Replies.Create(
            userId,
            communityId,
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
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Reply not found on comment");

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
            ?? throw new HttpCustomException(
            HttpStatusCode.NotFound, "Reply not found on comment");

        commentReply.RemoveReply(replyId);

        _dbContext.Entry(commentReply).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void AddReplyVote(CommentId id, ReplyId replyId, UserId userId, bool isVoted)
    {
        Comment commentReplyVotes =
            _dbContext.Comments
                .Include(cr => cr.Replies)
                .Where(cr => cr.Replies.Any(
                    r => r.Id == replyId && r.UserId == userId))
                .SingleOrDefault(c => c.Id == id)
                ?? throw new HttpCustomException(
                HttpStatusCode.NotFound, "Vote not found on reply");

        var replyVotes = RepliesVotes.Create(
                replyId,
                userId,
                isVoted
            );

        commentReplyVotes.AddReplyVote(replyId, replyVotes);

        _dbContext.Comments.Update(commentReplyVotes);

        _dbContext.Entry(commentReplyVotes).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void UpdateReplyVoteById(CommentId id, ReplyId replyId, VoteId voteId, UserId userId, bool isVoted)
    {
        Comment commentReplyVotes =
            _dbContext.Comments
                .Include(cr => cr.Replies)
                .Where(cr => cr.Replies.Any(
                    r => r.Id == replyId && r.UserId == userId))
                .SingleOrDefault(c => c.Id == id)
                ?? throw new HttpCustomException(
                HttpStatusCode.NotFound, "Vote not found on reply");

        commentReplyVotes.UpdateReplyVote(replyId, voteId, isVoted);

        _dbContext.Comments.Update(commentReplyVotes);

        _dbContext.Entry(commentReplyVotes).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void DeleteReplyVoteById(CommentId id, ReplyId replyId, VoteId voteId, UserId userId)
    {
        Comment commentReplyVotes =
            _dbContext.Comments
                .Include(cr => cr.Replies)
                .Where(cr => cr.Replies.Any(
                    r => r.Id == replyId && r.UserId == userId))
                .SingleOrDefault(c => c.Id == id)
                ?? throw new HttpCustomException(
                HttpStatusCode.NotFound, "Vote not found on reply");

        commentReplyVotes.RemoveReplyVote(replyId, voteId);

        _dbContext.Entry(commentReplyVotes).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }
}