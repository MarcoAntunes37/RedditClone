namespace RedditClone.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using RedditClone.Application.Persistence;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class PostRepository : IPostRepository
{
    private readonly RedditCloneDbContext _dbContext;

    public PostRepository(RedditCloneDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Post GetPostById(PostId postId)
    {
        Post post = _dbContext.Posts.FirstOrDefault(p => p.Id == postId)
        ?? throw new Exception("Invalid Post");

        return post;
    }

    public List<Post> GetPostListByUser(UserId userId)
    {

        List<Post> posts = _dbContext.Posts.Where(p => p.UserId == userId).ToList();

        return posts;
    }

    public List<Post> GetPostListByCommunity(CommunityId communityId)
    {

        List<Post> posts = _dbContext.Posts.Where(p => p.CommunityId == communityId).ToList();

        return posts;
    }

    public void Add(Post post)
    {
        _dbContext.Posts.Add(post);
        _dbContext.SaveChangesAsync();
    }

    public void UpdatePostById(PostId id, UserId userId, string title, string content)
    {
        Post post = _dbContext.Posts.SingleOrDefault(p => p.Id == id && p.UserId == userId)
            ?? throw new Exception("An error occurred, post is invalid or you not the owner");

        post.UpdatePost(title, content);

        _dbContext.Posts.Update(post);

        _dbContext.Entry(post).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void DeletePostById(PostId id, UserId userId)
    {
        Post post = _dbContext.Posts.SingleOrDefault(p => p.Id == id && p.UserId == userId)
            ?? throw new Exception("An error occurred, post is invalid or you not the owner");

        _dbContext.Posts.Remove(post);

        _dbContext.SaveChanges();
    }

    public void AddPostVote(PostId id, UserId userId, bool isVoted)
    {
        Post postVote = _dbContext.Posts
            .Include(p => p.Votes)
            .SingleOrDefault(p => p.Id == id)
            ?? throw new Exception("An error occurred, post is invalid.");

        var vote = Votes.Create(id, userId, isVoted);

        postVote.AddVote(vote);

        _dbContext.Posts.Update(postVote);

        _dbContext.Entry(postVote).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void UpdatePostVoteById(PostId id, VoteId voteId, UserId userId, bool isVoted)
    {
        Post postVote = _dbContext.Posts
            .Include(p => p.Votes)
            .Where(p => p.Votes.Any(pv => pv.Id == voteId && pv.UserId == userId))
            .SingleOrDefault(p => p.Id == id)
            ?? throw new Exception("An error occurred, post is invalid or vote in post is invalid");

        postVote.UpdateVote(voteId, isVoted);

        _dbContext.Entry(postVote).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }

    public void DeletePostVoteById(PostId id, VoteId voteId, UserId userId)
    {
        Post postVote = _dbContext.Posts
            .Include(p => p.Votes)
            .Where(p => p.Votes.Any(pv => pv.Id == voteId && pv.UserId == userId))
            .SingleOrDefault(p => p.Id == id)
            ?? throw new Exception("An error occurred, post is invalid or vote in post is invalid");

        postVote.RemoveVote(voteId);

        _dbContext.Entry(postVote).State = EntityState.Modified;

        _dbContext.SaveChanges();
    }
}