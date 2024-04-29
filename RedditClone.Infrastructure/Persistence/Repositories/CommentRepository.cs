namespace RedditClone.Infrastructure.Persistence.Repositories;

using ErrorOr;
using Serilog;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public class CommentRepository(RedditCloneDbContext dbContext) : ICommentRepository
{
    private readonly RedditCloneDbContext _dbContext = dbContext;

    public ErrorOr<Comment> GetCommentById(CommentId commentId)
    {
        Comment comment = _dbContext.Comments.FirstOrDefault(c => c.Id == commentId)!;

        if (comment is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        return comment;
    }

    public List<Comment> GetCommentsListByPostId(PostId postId)
    {
        List<Comment> comments = _dbContext.Comments.Where(c => c.PostId == postId).ToList();

        return comments;
    }

    public void Add(Comment comment)
    {
        _dbContext.Comments.Add(comment);
    }

    public ErrorOr<Comment> UpdateCommentById(CommentId id, UserId userId, string content)
    {
        Comment comment = _dbContext.Comments.SingleOrDefault(c => c.Id == id)!;

        if (comment is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        if(comment.UserId != userId)
        {
            Error error = Errors.Comments.CommentNotOwnerByUser;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        comment.UpdateComment(content);

        _dbContext.Comments.Update(comment);

        return comment;
    }

    public ErrorOr<bool> DeleteCommentById(CommentId id, UserId userId)
    {
        Comment comment = _dbContext.Comments.SingleOrDefault(c => c.Id == id)!;

        if (comment is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        if (comment.UserId != userId)
        {
            Error error = Errors.Comments.CommentNotOwnerByUser;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        comment.DeleteComment();

        _dbContext.Comments.Remove(comment);

        return true;
    }

    public ErrorOr<bool> AddCommentVote(CommentId id, UserId userId, bool isVoted)
    {
        Comment? commentVote = _dbContext.Comments.Include(p => p.Votes).SingleOrDefault(p => p.Id == id);

        if (commentVote is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        var vote = Votes.Create(
            id,
            userId,
            isVoted);

        commentVote.AddVote(vote);

        _dbContext.Comments.Update(commentVote);

        return true;
    }

    public ErrorOr<bool> UpdateCommentVoteById(CommentId id, VoteId voteId, UserId userId, bool isVoted)
    {
        Comment? commentVote = _dbContext.Comments.Include(p => p.Votes).Where(
            p => p.Votes.Any(pv => pv.Id == voteId && pv.UserId == userId)).SingleOrDefault(p => p.Id == id);

        if (commentVote is null)
        {
            Error error = Errors.CommentVotes.VoteNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        commentVote.UpdateVote(voteId, isVoted);

        return true;
    }

    public ErrorOr<bool> DeleteCommentVoteById(CommentId id, VoteId voteId, UserId userId)
    {
        Comment? commentVote = _dbContext.Comments
            .Include(p => p.Votes)
            .Where(p => p.Votes.Any(pv => pv.Id == voteId && pv.UserId == userId))
            .SingleOrDefault(p => p.Id == id);

        if (commentVote is null)
        {
            Error error = Errors.CommentVotes.VoteNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        commentVote.RemoveVote(voteId);

        return true;
    }

    public ErrorOr<bool> AddCommentReply(CommentId id, UserId userId, string content)
    {
        Comment? commentReply = _dbContext.Comments
            .Include(p => p.Replies)
            .SingleOrDefault(p => p.Id == id);

        if (commentReply is null)
        {
            Error error = Errors.CommentReplies.ReplyNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        List<RepliesVotes> repliesVotes = [];

        var reply = Replies.Create(
            userId,
            id,
            content,
            repliesVotes);

        commentReply.AddReply(reply);

        _dbContext.Comments.Update(commentReply);

        return true;
    }

    public ErrorOr<bool> UpdateCommentReplyById(CommentId id, ReplyId replyId, UserId userId, string content)
    {
        Comment? commentReply = _dbContext.Comments
            .Include(p => p.Replies)
            .Where(p => p.Replies.Any(pv => pv.Id == replyId && pv.UserId == userId))
            .SingleOrDefault(p => p.Id == id);

        if (commentReply is null)
        {
            Error error = Errors.CommentReplies.ReplyNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        commentReply.UpdateReply(replyId, content);

        return true;
    }

    public ErrorOr<bool> DeleteCommentReplyById(CommentId id, ReplyId replyId, UserId userId)
    {
        Comment? commentReply = _dbContext.Comments
            .Include(p => p.Replies).Where(
                p => p.Replies.Any(pv => pv.Id == replyId && pv.UserId == userId))
            .SingleOrDefault(p => p.Id == id);

        if (commentReply is null)
        {
            Error error = Errors.CommentReplies.ReplyNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        commentReply.RemoveReply(replyId);

        return true;
    }

    public ErrorOr<bool> AddReplyVote(CommentId id, ReplyId replyId, UserId userId, bool isVoted)
    {
        Comment? commentReplyVotes = _dbContext.Comments.Include(cr => cr.Replies).Where(
            cr => cr.Replies.Any(r => r.Id == replyId && r.UserId == userId))
            .SingleOrDefault(c => c.Id == id);

        if (commentReplyVotes is null)
        {
            Error error = Errors.CommentReplies.ReplyNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        var replyVotes = RepliesVotes.Create(
                replyId,
                userId,
                isVoted
            );

        commentReplyVotes.AddReplyVote(replyId, replyVotes);

        _dbContext.Comments.Update(commentReplyVotes);

        return true;
    }

    public ErrorOr<bool> UpdateReplyVoteById(CommentId id, ReplyId replyId, VoteId voteId, UserId userId, bool isVoted)
    {
        Comment? commentReplyVotes = _dbContext.Comments.Include(cr => cr.Replies).Where(
                cr => cr.Replies.Any(r => r.Id == replyId && r.UserId == userId))
            .SingleOrDefault(c => c.Id == id);

        if (commentReplyVotes is null)
        {
            Error error = Errors.CommentReplies.ReplyNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        commentReplyVotes.UpdateReplyVote(replyId, voteId, isVoted);

        _dbContext.Comments.Update(commentReplyVotes);

        return true;
    }

    public ErrorOr<bool> DeleteReplyVoteById(CommentId id, ReplyId replyId, VoteId voteId, UserId userId)
    {
        Comment? commentReplyVotes = _dbContext.Comments.Include(cr => cr.Replies).Where(
                cr => cr.Replies.Any(r => r.Id == replyId && r.UserId == userId))
            .SingleOrDefault(c => c.Id == id);

        if (commentReplyVotes is null)
        {
            Error error = Errors.CommentReplies.ReplyNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        commentReplyVotes.RemoveReplyVote(replyId, voteId);

        return true;
    }

    public ErrorOr<List<RepliesVotes>> GetVoteListByReplyId(CommentId commentId, ReplyId replyId)
    {
        Comment? comment = _dbContext.Comments
            .Include(p => p.Replies)
            .Where(p => p.Replies.Any(pv => pv.Id == replyId))
            .Include(p => p.Votes)
            .SingleOrDefault(p => p.Id == commentId);

        if (comment is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        return comment.Replies.FirstOrDefault()!.Votes.ToList();
    }

    public ErrorOr<List<Votes>> GetVoteListByCommentId(CommentId commentId)
    {
        Comment? comment = _dbContext.Comments
            .Include(p => p.Votes)
            .SingleOrDefault(p => p.Id == commentId);

        if (comment is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        return comment.Votes.ToList();
    }

    public ErrorOr<List<Replies>> GetReplyListByCommentId(CommentId commentId)
    {
        Comment? comment = _dbContext.Comments
            .Include(p => p.Replies)
            .SingleOrDefault(p => p.Id == commentId);

        if (comment is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        return comment.Replies.ToList();
    }

    public bool UserExists(UserId userId)
    {
        return _dbContext.Users.Any(u => u.Id == userId);
    }

    public bool CommunityExists(CommunityId communityId)
    {
        return _dbContext.Communities.Any(c => c.Id == communityId);
    }

    public bool PostExists(PostId postId)
    {
        return _dbContext.Posts.Any(p => p.Id == postId);
    }

    public bool CommentExists(CommentId commentId)
    {
        return _dbContext.Comments.Any(c => c.Id == commentId);
    }

    public bool CommentVoteExists(CommentId commentId, VoteId voteId)
    {
        return _dbContext.Comments.Find(commentId)!.Votes.Any(v => v.Id == voteId);
    }

    public bool CommentReplyExists(CommentId commentId, ReplyId replyId)
    {
        return _dbContext.Comments.Find(commentId)!.Replies.Any(r => r.Id == replyId);
    }

    public bool UserAlreadyVoted(CommentId commentId, UserId userId)
    {
        return _dbContext.Comments.Find(commentId)
            !.Votes.Any(v => v.UserId == userId && v.CommentId == commentId);
    }
}