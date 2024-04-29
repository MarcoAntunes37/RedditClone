namespace RedditClone.Tests.InfrastructureTests.Repository;

using System;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;

public class PostRepositoryTests
{
    [Fact]
    public void PostRepository_ShouldGetPostById_WhenPostExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var post = arrange["post"] as Post;

        using (var context = new RedditCloneDbContext(options!))
        {
            var postRepository = new PostRepository(context);

            postRepository.Add(post!);

            context.SaveChanges();

            var result = postRepository.GetPostById(post!.Id);

            Assert.NotNull(result.Value);
        }
    }

    [Fact]
    public void PostRepository_ShouldGetPostsListByCommunity_WhenPostsExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var post = arrange["post"] as Post;

        using (var context = new RedditCloneDbContext(options!))
        {
            var postRepository = new PostRepository(context);

            postRepository.Add(post!);

            context.SaveChanges();

            var result = postRepository.GetPostListByCommunity(post!.CommunityId);

            Assert.NotEmpty(result);
        }
    }
    [Fact]
    public void PostRepository_ShouldGetPostsListByUser_WhenPostsExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var post = arrange["post"] as Post;

        using (var context = new RedditCloneDbContext(options!))
        {
            var postRepository = new PostRepository(context);

            postRepository.Add(post!);

            context.SaveChanges();

            var result = postRepository.GetPostListByUser(post!.UserId);

            Assert.NotEmpty(result);
        }
    }

    [Fact]
    public void PostRepository_ShouldAddPost_WhenPostIsValid()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var post = arrange["post"] as Post;

        using (var context = new RedditCloneDbContext(options!))
        {
            var postRepository = new PostRepository(context);

            postRepository.Add(post!);

            context.SaveChanges();

            Assert.Equal(1, context.Posts.Count());
        }
    }

    [Fact]
    public void PostRepository_ShouldUpdatePost_WhenPostExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var post = arrange["post"] as Post;

        using (var context = new RedditCloneDbContext(options!))
        {
            var postRepository = new PostRepository(context);

            postRepository.Add(post!);

            context.SaveChanges();

            postRepository.UpdatePostById(
                post!.Id,
                arrange["newTitle"].ToString()!,
                arrange["newContent"].ToString()!);

            context.SaveChanges();

            Assert.Equal(1, context.Posts.Count());

            Assert.Equal(arrange["newTitle"], post.Title);

            Assert.Equal(arrange["newContent"], post.Content);
        }
    }

    [Fact]
    public void PostRepository_ShouldDeletePost_WhenPostExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var post = arrange["post"] as Post;

        using (var context = new RedditCloneDbContext(options!))
        {
            var postRepository = new PostRepository(context);

            postRepository.Add(post!);

            context.SaveChanges();

            postRepository.DeletePostById(post!.Id, post.UserId!);

            context.SaveChanges();

            Assert.Equal(0, context.Posts.Count());
        }
    }

    [Fact]
    public void PostRepository_ShouldAddVoteToPost_WhenVoteIsValid()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var post = arrange["post"] as Post;

        using (var context = new RedditCloneDbContext(options!))
        {
            var postRepository = new PostRepository(context);

            postRepository.Add(post!);

            context.SaveChanges();

            postRepository.AddPostVote(post!.Id, post.UserId, true);

            context.SaveChanges();

            Assert.Single(post.Votes);
        }
    }

    [Fact]
    public void PostRepository_ShouldUpdateVote_WhenVoteIsValid()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var post = arrange["post"] as Post;

        using (var context = new RedditCloneDbContext(options!))
        {
            var postRepository = new PostRepository(context);

            postRepository.Add(post!);

            context.SaveChanges();

            postRepository.AddPostVote(post!.Id, post.UserId, true);

            context.SaveChanges();

            var vote = post.Votes.First();

            postRepository.UpdatePostVoteById(
                post.Id,
                vote.Id,
                vote.UserId,
                false);

            context.SaveChanges();

            var votesList = post.Votes.ToList();

            Assert.False(post.Votes.First().IsVoted);
        }
    }

    [Fact]
    public void PostRepository_ShouldRemoveVoteFromPost_WhenVoteExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var post = arrange["post"] as Post;

        using (var context = new RedditCloneDbContext(options!))
        {
            var postRepository = new PostRepository(context);

            postRepository.Add(post!);

            context.SaveChanges();

            postRepository.AddPostVote(post!.Id, post.UserId, true);

            context.SaveChanges();

            var vote = post.Votes.First();

            postRepository.DeletePostVoteById(post.Id, vote.Id, vote.UserId);

            context.SaveChanges();

            Assert.Empty(post.Votes);
        }
    }

    private static Dictionary<string, object> CreateTestArranges()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var userId = Guid.NewGuid();

        var communityId = Guid.NewGuid();

        var title = "TestTitle";

        var content = "TestContent";

        var newTitle = "UpdatedTitle";

        var newContent = "UpdatedContent";

        List<Votes> votes = new();

        var post = Post.Create(
            new CommunityId(communityId),
            new UserId(userId),
            title,
            content,
            votes);

        Dictionary<string, object> arrange = new()
        {
            { "post", post },
            { "options", options },
            { "title", title },
            { "content", content },
            { "newTitle", newTitle },
            { "newContent", newContent },
            { "votes", votes }
        };

        return arrange;
    }
}
