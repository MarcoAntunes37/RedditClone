namespace RedditClone.Application.Persistence;

using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public interface ICommentRepository
{
    void Add(Comment comment);
    void UpdateCommentById(CommentId id, UserId userId, string content);
    void DeleteCommentById(CommentId id, UserId userId);
    void AddCommentVote(CommentId id, UserId userId, bool isVoted);
    void UpdateCommentVoteById(CommentId id, VoteId voteId, UserId userId, bool isVoted);
    void DeleteCommentVoteById(CommentId id, VoteId voteId, UserId userId);
}