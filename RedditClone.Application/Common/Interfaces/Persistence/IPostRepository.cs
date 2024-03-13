namespace RedditClone.Application.Persistence;

using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public interface IPostRepository
{
    Post GetPostById(PostId postId);
    List<Post> GetPostListByUser(UserId userId);
    List<Post> GetPostListByCommunity(CommunityId communityId);
    void Add(Post post);
    void UpdatePostById(PostId id, UserId userId, string title, string content);
    void DeletePostById(PostId id, UserId userId);
    void AddPostVote(PostId postId, UserId userId, bool IsVoted);
    void UpdatePostVoteById(PostId id, VoteId voteId, UserId userId, bool isVoted);
    void DeletePostVoteById(PostId id, VoteId voteId, UserId userId);
}