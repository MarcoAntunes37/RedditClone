using System.Configuration.Assemblies;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.Tests;

public class DomainCommentsTests
{
    [Fact]
    public void ShouldReturnCommentObjectOnCreate()
    {
        Guid postId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        var content = "This is a comment content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();
        List<Replies> replies = new();

        var comment = Comment.Create(
            new UserId(userId),
            new PostId(postId),
            content,
            createdAt,
            updatedAt,
            votes,
            replies);

        Assert.NotNull(comment);
        Assert.Equal(new UserId(userId), comment.UserId);
        Assert.Equal(new PostId(postId), comment.PostId);
        Assert.Equal(content, comment.Content);
        Assert.Equal(createdAt, comment.CreatedAt);
        Assert.Equal(updatedAt, comment.UpdatedAt);
        Assert.Empty(votes);
        Assert.Empty(replies);
    }


    [Fact]
    public void ShouldReturnCommentObjectWithNewDataOnUpdate()
    {
        Guid postId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        var content = "This is a comment content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        var newContent = "This is a comment content updated";

        List<Votes> votes = new();
        List<Replies> replies = new();

        var comment = Comment.Create(
            new UserId(userId),
            new PostId(postId),
            content,
            createdAt,
            updatedAt,
            votes,
            replies);

        var oldUpdatedAt = comment.UpdatedAt;
        comment.UpdateComment(newContent);

        Assert.NotNull(comment);
        Assert.Equal(newContent, comment.Content);
        Assert.True(oldUpdatedAt < comment.UpdatedAt);
    }

    [Fact]
    public void ShouldReturnCommentObjectWithAnEntryInVotesList()
    {
        Guid postId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        Guid voteUserId = new("e041fd03-f6f1-4d79-8d25-b81f6d9cfbec");
        var content = "This is a comment content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();
        List<Replies> replies = new();

        var comment = Comment.Create(
            new UserId(userId),
            new PostId(postId),
            content,
            createdAt,
            updatedAt,
            votes,
            replies);

        var vote = Votes.Create(
            comment.Id,
            new UserId(voteUserId),
            true);

        comment.AddVote(vote);

        Assert.NotNull(comment.Votes);
        Assert.NotEmpty(comment.Votes);
        Assert.True(vote.IsVoted);
    }

    [Fact]
    public void ShouldReturnCommentObjectWithVotesListEntryDataUpdated()
    {
        Guid postId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        Guid voteUserId = new("e041fd03-f6f1-4d79-8d25-b81f6d9cfbec");
        var content = "This is a comment content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();
        List<Replies> replies = new();

        var comment = Comment.Create(
            new UserId(userId),
            new PostId(postId),
            content,
            createdAt,
            updatedAt,
            votes,
            replies);

        var vote = Votes.Create(
            comment.Id,
            new UserId(voteUserId),
            true);

        comment.AddVote(vote);

        comment.UpdateVote(vote.Id, false);

        Assert.NotNull(comment.Votes);
        Assert.NotEmpty(comment.Votes);
        Assert.False(vote.IsVoted);
    }
    //Remove vote
    [Fact]
    public void ShouldReturnCommentObjectWithVotesListEntryDataRemoved()
    {
        Guid postId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        Guid voteUserId = new("e041fd03-f6f1-4d79-8d25-b81f6d9cfbec");
        var content = "This is a comment content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();
        List<Replies> replies = new();

        var comment = Comment.Create(
            new UserId(userId),
            new PostId(postId),
            content,
            createdAt,
            updatedAt,
            votes,
            replies);

        var vote = Votes.Create(
            comment.Id,
            new UserId(voteUserId),
            true);

        comment.AddVote(vote);

        comment.RemoveVote(vote.Id);

        Assert.NotNull(comment.Votes);
        Assert.Empty(comment.Votes);
    }

    [Fact]
    public void ShouldReturnCommentObjectWithAnEntryInRepliesList()
    {
        Guid postId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        Guid replyUserId = new("3fff8262-ee3c-465b-8526-e678bcf50804");
        var replyContent = "This is a reply content example";
        var content = "This is a comment content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();
        List<Replies> replies = new();
        List<RepliesVotes> repliesVotes = new();

        var comment = Comment.Create(
            new UserId(userId),
            new PostId(postId),
            content,
            createdAt,
            updatedAt,
            votes,
            replies);

        var reply = Replies.Create(
            new UserId(replyUserId),
            comment.Id,
            replyContent,
            DateTime.UtcNow,
            DateTime.UtcNow,
            repliesVotes);

        comment.AddReply(reply);

        Assert.NotNull(comment.Replies);
        Assert.NotEmpty(comment.Replies);
        Assert.Equal(comment.Replies[0], reply);
    }

    [Fact]
    public void ShouldReturnCommentObjectWithAnEntryInRepliesListDataUpdated()
    {
        Guid postId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        Guid replyUserId = new("3fff8262-ee3c-465b-8526-e678bcf50804");
        var replyContent = "This is a reply content example";
        var content = "This is a comment content example";
        var newContent = "This is a comment content example updated";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();
        List<Replies> replies = new();
        List<RepliesVotes> repliesVotes = new();

        var comment = Comment.Create(
            new UserId(userId),
            new PostId(postId),
            content,
            createdAt,
            updatedAt,
            votes,
            replies);

        var reply = Replies.Create(
            new UserId(replyUserId),
            comment.Id,
            replyContent,
            DateTime.UtcNow,
            DateTime.UtcNow,
            repliesVotes);

        comment.AddReply(reply);

        comment.UpdateReply(reply.Id, newContent);

        Assert.NotNull(comment.Replies);
        Assert.NotEmpty(comment.Replies);
        Assert.Equal(comment.Replies[0].Content, newContent);
    }

    [Fact]
    public void ShouldReturnCommentObjectWithAnEntryInRepliesListDataRemoved()
    {
        Guid postId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        Guid replyUserId = new("3fff8262-ee3c-465b-8526-e678bcf50804");
        var replyContent = "This is a reply content example";
        var content = "This is a comment content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();
        List<Replies> replies = new();
        List<RepliesVotes> repliesVotes = new();

        var comment = Comment.Create(
            new UserId(userId),
            new PostId(postId),
            content,
            createdAt,
            updatedAt,
            votes,
            replies);

        var reply = Replies.Create(
            new UserId(replyUserId),
            comment.Id,
            replyContent,
            DateTime.UtcNow,
            DateTime.UtcNow,
            repliesVotes);

        comment.AddReply(reply);

        comment.RemoveReply(reply.Id);

        Assert.NotNull(comment.Replies);
        Assert.Empty(comment.Replies);
    }

    [Fact]
    public void ShouldReturnCommentObjectWithAnEntryInRepliesVotesList()
    {
        Guid postId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        Guid replyUserId = new("3fff8262-ee3c-465b-8526-e678bcf50804");
        Guid voteUserId = new("cef60ad4-6361-4271-a5d7-b0166b5c8f3b");
        var replyContent = "This is a reply content example";
        var content = "This is a comment content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();
        List<Replies> replies = new();
        List<RepliesVotes> repliesVotes = new();

        var comment = Comment.Create(
            new UserId(userId),
            new PostId(postId),
            content,
            createdAt,
            updatedAt,
            votes,
            replies);

        var reply = Replies.Create(
            new UserId(replyUserId),
            comment.Id,
            replyContent,
            DateTime.UtcNow,
            DateTime.UtcNow,
            repliesVotes);

        comment.AddReply(reply);

        var replyVote = RepliesVotes.Create(
            reply.Id,
            new UserId(voteUserId),
            true);

        reply.AddReplyVote(replyVote);

        Assert.NotNull(comment.Replies[0].Votes);
        Assert.NotEmpty(comment.Replies[0].Votes);
        Assert.Equal(comment.Replies[0].Votes[0], replyVote);
    }

    [Fact]
    public void ShouldReturnCommentObjectWithAnEntryInRepliesVotesListDataUpdated()
    {
        Guid postId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        Guid replyUserId = new("3fff8262-ee3c-465b-8526-e678bcf50804");
        Guid voteUserId = new("cef60ad4-6361-4271-a5d7-b0166b5c8f3b");
        var replyContent = "This is a reply content example";
        var content = "This is a comment content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();
        List<Replies> replies = new();
        List<RepliesVotes> repliesVotes = new();

        var comment = Comment.Create(
            new UserId(userId),
            new PostId(postId),
            content,
            createdAt,
            updatedAt,
            votes,
            replies);

        var reply = Replies.Create(
            new UserId(replyUserId),
            comment.Id,
            replyContent,
            DateTime.UtcNow,
            DateTime.UtcNow,
            repliesVotes);

        comment.AddReply(reply);

        var replyVote = RepliesVotes.Create(
            reply.Id,
            new UserId(voteUserId),
            true);

        reply.AddReplyVote(replyVote);

        reply.UpdateReplyVote(replyVote.Id, false);

        Assert.NotNull(comment.Replies[0].Votes);
        Assert.NotEmpty(comment.Replies[0].Votes);
        Assert.Equal(comment.Replies[0].Votes[0], replyVote);
        Assert.False(comment.Replies[0].Votes[0].IsVoted);
    }

    [Fact]
    public void ShouldReturnCommentObjectWithAnEntryInRepliesVotesListDataRemoved()
    {
        Guid postId = new("e24abca2-f98e-4c8c-9825-9402282c6014");
        Guid userId = new("89d5caf3-e1f3-402d-a84b-fc3f442d3ca3");
        Guid replyUserId = new("3fff8262-ee3c-465b-8526-e678bcf50804");
        Guid voteUserId = new("cef60ad4-6361-4271-a5d7-b0166b5c8f3b");
        var replyContent = "This is a reply content example";
        var content = "This is a comment content example";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        List<Votes> votes = new();
        List<Replies> replies = new();
        List<RepliesVotes> repliesVotes = new();

        var comment = Comment.Create(
            new UserId(userId),
            new PostId(postId),
            content,
            createdAt,
            updatedAt,
            votes,
            replies);

        var reply = Replies.Create(
            new UserId(replyUserId),
            comment.Id,
            replyContent,
            DateTime.UtcNow,
            DateTime.UtcNow,
            repliesVotes);

        comment.AddReply(reply);

        var replyVote = RepliesVotes.Create(
            reply.Id,
            new UserId(voteUserId),
            true);

        reply.AddReplyVote(replyVote);

        // Assert.NotNull(comment.Replies[0].Votes);
        // Assert.Empty(comment.Replies[0].Votes);
    }
}