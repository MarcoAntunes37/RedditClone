namespace RedditClone.Tests.DomainTests;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.PostAggregate.DomainEvents;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public class DomainPostTests
{
    [Fact]
    public void Create_Post_ValidInput_Success()
    {
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        var title = "This is a post title example";
        var content = "This is a post content example";

        List<Votes> votes = new();

        var post = Post.Create(
            new CommunityId(communityId),
            new UserId(userId),
            title,
            content,
            votes);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)post.GetDomainEvents();

        Assert.NotNull(post);
        Assert.IsType<PostCreatedDomainEvent>(domainEvents.LastOrDefault());
        Assert.Equal(post.CommunityId, new CommunityId(communityId));
        Assert.Equal(post.UserId, new UserId(userId));
        Assert.Equal(post.Title, title);
        Assert.Equal(post.Content, content);
        Assert.Equal(post.Votes, votes);
    }

    [Fact]
    public void Update_Post_ValidInput_Success()
    {
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        var title = "This is a post title example";
        var content = "This is a post content example";
        var newTitle = "This is a post updated title example";
        var newContent = "This is a post updated content example";

        List<Votes> votes = new();

        var post = Post.Create(
            new CommunityId(communityId),
            new UserId(userId),
            title,
            content,
            votes);

        post.UpdatePost(newTitle, newContent);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)post.GetDomainEvents();

        Assert.NotNull(post);
        Assert.IsType<PostUpdatedDomainEvent>(domainEvents.LastOrDefault());
        Assert.Equal(post.Title, newTitle);
        Assert.Equal(post.Content, newContent);
    }

    [Fact]
    public void Delete_Post_ValidInput_Success()
    {
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();

        var title = "This is a post title example";
        var content = "This is a post content example";

        List<Votes> votes = new();

        var post = Post.Create(
            new CommunityId(communityId),
            new UserId(userId),
            title,
            content,
            votes);

        post.DeletePost();

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)post.GetDomainEvents();

        Assert.NotNull(post);
        Assert.IsType<PostDeletedDomainEvent>(domainEvents.LastOrDefault());
    }

    [Fact]
    public void Create_Vote_ValidInput_Success()
    {
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        Guid voteUserId = Guid.NewGuid();
        var title = "This is a post title example";
        var content = "This is a post content example";
        List<Votes> votes = new();

        var post = Post.Create(
            new CommunityId(communityId),
            new UserId(userId),
            title,
            content,
            votes);

        var vote = Votes.Create(
            post.Id,
            new UserId(voteUserId),
            true);

        post.AddVote(vote);

        List<IDomainEvent> postDomainEvents = (List<IDomainEvent>)post.GetDomainEvents();
        List<IDomainEvent> voteDomainEvents = (List<IDomainEvent>)vote.GetDomainEvents();

        Assert.NotNull(post.Votes);
        Assert.NotEmpty(post.Votes);
        Assert.IsType<PostCreatedDomainEvent>(postDomainEvents.FirstOrDefault());
        Assert.IsType<VoteCreatedDomainEvent>(voteDomainEvents.FirstOrDefault());

    }

    [Fact]
    public void Update_Vote_ValidInput_Success()
    {
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        var title = "This is a post title example";
        var content = "This is a post content example";
        var eventVotesCounter = 0;

        List<Votes> votes = new();

        var post = Post.Create(
            new CommunityId(communityId),
            new UserId(userId),
            title,
            content,
            votes);

        var vote = Votes.Create(
            post.Id,
            post.UserId,
            true
        );

        eventVotesCounter++;

        post.AddVote(vote);

        vote.UpdateVote(false);

        eventVotesCounter++;

        var firstOneVote = post.Votes.FirstOrDefault();

        List<IDomainEvent> postDomainEvents = (List<IDomainEvent>)post.GetDomainEvents();
        List<IDomainEvent> voteDomainEvents = (List<IDomainEvent>)vote.GetDomainEvents();

        Assert.NotNull(post.Votes);
        Assert.IsType<PostCreatedDomainEvent>(postDomainEvents.FirstOrDefault());
        Assert.IsType<VoteUpdatedDomainEvent>(voteDomainEvents[eventVotesCounter-1]);
        Assert.NotEmpty(post.Votes);
        Assert.Equal(firstOneVote, vote);
        Assert.False(firstOneVote?.IsVoted);
    }

    [Fact]
    public void Delete_Vote_ValidInput_Success()
    {
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        Guid voteUserId = Guid.NewGuid();
        var title = "This is a post title example";
        var content = "This is a post content example";

        List<Votes> votes = new();

        var post = Post.Create(
            new CommunityId(communityId),
            new UserId(userId),
            title,
            content,
            votes);

        var vote = Votes.Create(
            post.Id,
            new UserId(voteUserId),
            true);

        post.AddVote(vote);

        post.RemoveVote(vote.Id);

        List<IDomainEvent> postDomainEvents = (List<IDomainEvent>)post.GetDomainEvents();
        List<IDomainEvent> voteDomainEvents = (List<IDomainEvent>)vote.GetDomainEvents();

        Assert.NotNull(post.Votes);
        Assert.IsType<PostCreatedDomainEvent>(postDomainEvents.LastOrDefault());
        Assert.IsType<VoteDeletedDomainEvent>(voteDomainEvents.LastOrDefault());
        Assert.Empty(post.Votes);
    }
}