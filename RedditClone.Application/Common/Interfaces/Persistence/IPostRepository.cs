namespace RedditClone.Application.Persistence;

using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public interface IPostRepository
{
    void Add(Post post);
    void UpdatePostById(PostId id, UserId userId, string title, string content);
    void DeletePostById(PostId id, UserId userId);
    void AddPostVote(PostId postId, UserId userId, bool IsVoted);
    void UpdatePostVoteById(PostId id, VoteId voteId, UserId userId, bool isVoted);
    public void DeletePostVoteById(PostId id, VoteId voteId, UserId userId);
}