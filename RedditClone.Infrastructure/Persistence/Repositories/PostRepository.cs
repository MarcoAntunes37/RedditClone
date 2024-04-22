namespace RedditClone.Infrastructure.Persistence.Repositories;

using ErrorOr;
using Serilog;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.Common.Errors;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Common.Interfaces.Persistence;

public class PostRepository(RedditCloneDbContext dbContext)
    : IPostRepository
{
    private readonly RedditCloneDbContext _dbContext = dbContext;

    public ErrorOr<Post> GetPostById(PostId postId)
    {
        Post? post = _dbContext.Posts.FirstOrDefault(p => p.Id == postId);

        if (post is null)
        {
            Error error = Errors.Posts.PostNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        return post;
    }

    public List<Votes> GetVotesListsByPostId(PostId postId)
    {
        List<Votes> votes = _dbContext.Posts.FirstOrDefault(p => p.Id == postId)!.Votes.ToList();

        return votes;
    }

    public List<Post> GetPostListByUser(UserId userId)
    {
        List<Post> posts = _dbContext.Posts.Where(p => p.UserId == userId)
        .Include(p => p.Votes)
        .ToList();

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
    }

    public ErrorOr<Post> UpdatePostById(PostId id, UserId userId, string title, string content)
    {
        Post? post = _dbContext.Posts.SingleOrDefault(p => p.Id == id && p.UserId == userId);

        if (post is null)
        {
            Error error = Errors.Posts.PostNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        post.UpdatePost(title, content);

        _dbContext.Posts.Update(post);

        return post;
    }

    public ErrorOr<bool> DeletePostById(PostId id, UserId userId)
    {
        Post? post = _dbContext.Posts.SingleOrDefault(p => p.Id == id && p.UserId == userId);

        if (post == null)
        {
            Error error = Errors.Posts.PostNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        _dbContext.Posts.Remove(post);

        return true;
    }

    public ErrorOr<bool> AddPostVote(PostId id, UserId userId, bool isVoted)
    {
        Post? postVote = _dbContext.Posts
            .Include(p => p.Votes)
            .SingleOrDefault(p => p.Id == id);

        if (postVote == null)
        {
            Error error = Errors.Posts.PostNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        var vote = Votes.Create(id, userId, isVoted);

        postVote.AddVote(vote);

        _dbContext.Posts.Update(postVote);

        return true;
    }

    public ErrorOr<bool> UpdatePostVoteById(PostId id, VoteId voteId, UserId userId, bool isVoted)
    {
        Post? postVote = _dbContext.Posts
            .Include(p => p.Votes)
            .Where(p => p.Votes.Any(pv => pv.Id == voteId && pv.UserId == userId))
            .SingleOrDefault(p => p.Id == id);

        if (postVote == null)
        {
            Error error = Errors.Posts.VoteNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        postVote.UpdateVote(voteId, isVoted);

        return true;
    }

    public ErrorOr<bool> DeletePostVoteById(PostId id, VoteId voteId, UserId userId)
    {
        Post? postVote = _dbContext.Posts
            .Include(p => p.Votes)
            .Where(p => p.Votes.Any(pv => pv.Id == voteId && pv.UserId == userId))
            .SingleOrDefault(p => p.Id == id);

        if(postVote == null)
        {
            Error error = Errors.Posts.VoteNotFound;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        _dbContext.Posts.Remove(postVote);;

        postVote.RemoveVote(voteId);

        return true;
    }
}