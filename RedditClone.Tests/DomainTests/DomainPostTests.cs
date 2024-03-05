namespace RedditClone.Tests;

using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class DomainPostTests
{
    [Fact]
    public void ShouldReturnPostObjectOnCreate()
    {
        Guid communityId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        var title = "This is a post title example";
        var content = "This is a post content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();

        var post = Post.Create(
            new CommunityId(communityId),
            new UserId(userId),
            title,
            content,
            createdAt,
            updatedAt,
            votes);

        Assert.NotNull(post);
        Assert.Equal(post.CommunityId, new CommunityId(communityId));
        Assert.Equal(post.UserId, new UserId(userId));
        Assert.Equal(post.Title, title);
        Assert.Equal(post.Content, content);
        Assert.Equal(post.CreatedAt, createdAt);
        Assert.Equal(post.UpdatedAt, updatedAt);
        Assert.Equal(post.Votes, votes);
    }

    [Fact]
    public void ShouldReturnPostObjectWithNewDataOnUpdate()
    {
        Guid communityId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        var title = "This is a post title example";
        var content = "This is a post content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();

        var post = Post.Create(
            new CommunityId(communityId),
            new UserId(userId),
            title,
            content,
            createdAt,
            updatedAt,
            votes);

        var oldUpdatedAt = post.UpdatedAt;
        var newTitle = "This is a post updated title example";
        var newContent = "This is a post updated content example";

        post.UpdatePost(newTitle, newContent);

        Assert.NotNull(post);
        Assert.Equal(post.Title, newTitle);
        Assert.Equal(post.Content, newContent);
        Assert.True(post.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void ShouldReturnPostObjectWithAnEntryInVotesList()
    {
        Guid communityId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        Guid voteUserId = new("e041fd03-f6f1-4d79-8d25-b81f6d9cfbec");
        var title = "This is a post title example";
        var content = "This is a post content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();

        var post = Post.Create(
            new CommunityId(communityId),
            new UserId(userId),
            title,
            content,
            createdAt,
            updatedAt,
            votes);

        var vote = Votes.Create(
            post.Id,
            new UserId(voteUserId),
            true
        );

        post.AddVote(vote);

        Assert.NotNull(post.Votes);
        Assert.NotEmpty(post.Votes);
    }

    [Fact]
    public void ShouldReturnPostObjectWithVotesListEntryDataUpdated()
    {
        Guid communityId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        var title = "This is a post title example";
        var content = "This is a post content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();

        var post = Post.Create(
            new CommunityId(communityId),
            new UserId(userId),
            title,
            content,
            createdAt,
            updatedAt,
            votes);

        var vote = Votes.Create(
            post.Id,
            post.UserId,
            true
        );

        post.AddVote(vote);

        vote.UpdateVote(false);

        Assert.NotNull(post.Votes);
        Assert.NotEmpty(post.Votes);
        Assert.Equal(post.Votes[0], vote);
        Assert.False(post.Votes[0].IsVoted);
    }

    [Fact]
    public void ShouldReturnPostObjectWithVotesListEntryDataRemoved()
    {
        Guid communityId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        Guid voteUserId = new("3fff8262-ee3c-465b-8526-e678bcf50804");
        var title = "This is a post title example";
        var content = "This is a post content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();

        var post = Post.Create(
            new CommunityId(communityId),
            new UserId(userId),
            title,
            content,
            createdAt,
            updatedAt,
            votes);

        var vote = Votes.Create(
            post.Id,
            new UserId(voteUserId),
            true
        );

        post.AddVote(vote);

        post.RemoveVote(vote.Id);

        Assert.NotNull(post.Votes);
        Assert.Empty(post.Votes);
    }
}