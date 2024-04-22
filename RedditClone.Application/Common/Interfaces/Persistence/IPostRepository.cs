namespace RedditClone.Application.Common.Interfaces.Persistence;

using ErrorOr;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public interface IPostRepository
{
    ErrorOr<Post> GetPostById(PostId postId);
    List<Post> GetPostListByUser(UserId userId);
    List<Votes> GetVotesListsByPostId(PostId postId);
    List<Post> GetPostListByCommunity(CommunityId communityId);
    void Add(Post post);
    ErrorOr<Post> UpdatePostById(PostId id, UserId userId, string title, string content);
    ErrorOr<bool> DeletePostById(PostId id, UserId userId);
    ErrorOr<bool> AddPostVote(PostId id, UserId userId, bool isVoted);
    ErrorOr<bool> UpdatePostVoteById(PostId id, VoteId voteId, UserId userId, bool isVoted);
    ErrorOr<bool> DeletePostVoteById(PostId id, VoteId voteId, UserId userId);
}