namespace RedditClone.Tests.DomainTests;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.DomainEvents;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public class DomainCommentsTests
{
    [Fact]
    public void Create_Comment_ValidInput_Success()
    {
        Guid postId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        var content = "This is a comment content example";

        List<Votes> votes = new();
        List<Replies> replies = new();

        var comment = Comment.Create(
            new UserId(userId),
            new CommunityId(communityId),
            new PostId(postId),
            content,
            votes,
            replies);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)comment.GetDomainEvents();

        Assert.NotNull(comment);
        Assert.IsType<CommentCreatedDomainEvent>(domainEvents.LastOrDefault());
        Assert.Equal(new UserId(userId), comment.UserId);
        Assert.Equal(new PostId(postId), comment.PostId);
        Assert.Equal(content, comment.Content);
        Assert.Empty(votes);
        Assert.Empty(replies);
    }


    [Fact]
    public void Update_Comment_ValidInput_Success()
    {
        Guid postId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        var content = "This is a comment content example";

        var newContent = "This is a comment content updated";

        List<Votes> votes = new();
        List<Replies> replies = new();

        var comment = Comment.Create(
            new UserId(userId),
            new CommunityId(communityId),
            new PostId(postId),
            content,
            votes,
            replies);

        comment.UpdateComment(newContent);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)comment.GetDomainEvents();

        Assert.NotNull(comment);
        Assert.Equal(newContent, comment.Content);
        Assert.IsType<CommentUpdatedDomainEvent>(domainEvents.LastOrDefault());
    }

    [Fact]
    public void Delete_Comment_ValidInput_Success()
    {
        Guid postId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        List<Votes> votes = new();
        List<Replies> replies = new();

        var comment = Comment.Create(
            new UserId(userId),
            new CommunityId(communityId),
            new PostId(postId),
            "This is a comment content example",
            votes,
            replies);

        comment.DeleteComment();

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)comment.GetDomainEvents();

        Assert.NotNull(comment);
        Assert.IsType<CommentDeletedDomainEvent>(domainEvents.LastOrDefault());
    }

    [Fact]
    public void Create_Vote_ValidInput_Success()
    {
        Guid postId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        Guid voteUserId = Guid.NewGuid();
        var content = "This is a comment content example";

        List<Votes> votes = new();
        List<Replies> replies = new();

        var comment = Comment.Create(
            new UserId(userId),
            new CommunityId(communityId),
            new PostId(postId),
            content,
            votes,
            replies);

        var vote = Votes.Create(
            comment.Id,
            new UserId(voteUserId),
            true);

        comment.AddVote(vote);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)vote.GetDomainEvents();

        Assert.NotNull(comment.Votes);
        Assert.NotEmpty(comment.Votes);
        Assert.True(vote.IsVoted);
        Assert.IsType<VoteCreatedDomainEvent>(domainEvents.LastOrDefault());
    }

    [Fact]
    public void Update_Vote_ValidInput_Success()
    {
        Guid postId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        Guid voteUserId = Guid.NewGuid();
        var content = "This is a comment content example";

        List<Votes> votes = new();
        List<Replies> replies = new();

        var comment = Comment.Create(
            new UserId(userId),
            new CommunityId(communityId),
            new PostId(postId),
            content,
            votes,
            replies);

        var vote = Votes.Create(
            comment.Id,
            new UserId(voteUserId),
            true);

        comment.AddVote(vote);

        comment.UpdateVote(vote.Id, false);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)vote.GetDomainEvents();

        Assert.NotNull(comment.Votes);
        Assert.NotEmpty(comment.Votes);
        Assert.False(vote.IsVoted);
        Assert.IsType<VoteUpdatedDomainEvent>(domainEvents.LastOrDefault());
    }

    [Fact]
    public void Delete_Vote_ValidInput_Success()
    {
        Guid postId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        Guid voteUserId = Guid.NewGuid();
        var content = "This is a comment content example";

        List<Votes> votes = new();
        List<Replies> replies = new();

        var comment = Comment.Create(
            new UserId(userId),
            new CommunityId(communityId),
            new PostId(postId),
            content,
            votes,
            replies);

        var vote = Votes.Create(
            comment.Id,
            new UserId(voteUserId),
            true);

        comment.AddVote(vote);

        comment.RemoveVote(vote.Id);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)vote.GetDomainEvents();

        Assert.NotNull(comment.Votes);
        Assert.Empty(comment.Votes);
        Assert.IsType<VoteDeletedDomainEvent>(domainEvents.LastOrDefault());
    }

    [Fact]
    public void Create_Reply_ValidInput_Success()
    {
        Guid postId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        Guid replyUserId = Guid.NewGuid();
        var replyContent = "This is a reply content example";
        var content = "This is a comment content example";

        List<Votes> votes = new();
        List<Replies> replies = new();
        List<RepliesVotes> repliesVotes = new();

        var comment = Comment.Create(
            new UserId(userId),
            new CommunityId(communityId),
            new PostId(postId),
            content,
            votes,
            replies);

        var reply = Replies.Create(
            new UserId(replyUserId),
            new CommunityId(communityId),
            comment.Id,
            replyContent,
            repliesVotes);

        comment.AddReply(reply);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)reply.GetDomainEvents();

        Assert.NotNull(comment.Replies);
        Assert.NotEmpty(comment.Replies);
        Assert.IsType<ReplyCreatedDomainEvent>(domainEvents.LastOrDefault());
        Assert.Equal(comment.Replies.FirstOrDefault(), reply);
    }

    [Fact]
    public void Update_Reply_ValidInput_Success()
    {
        Guid postId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        Guid replyUserId = Guid.NewGuid();
        var replyContent = "This is a reply content example";
        var content = "This is a comment content example";
        var newContent = "This is a comment content example updated";

        List<Votes> votes = new();
        List<Replies> replies = new();
        List<RepliesVotes> repliesVotes = new();

        var comment = Comment.Create(
            new UserId(userId),
            new CommunityId(communityId),
            new PostId(postId),
            content,
            votes,
            replies);

        var reply = Replies.Create(
            new UserId(replyUserId),
            new CommunityId(communityId),
            comment.Id,
            replyContent,
            repliesVotes);

        comment.AddReply(reply);

        comment.UpdateReply(reply.Id, newContent);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)reply.GetDomainEvents();

        Assert.NotNull(comment.Replies);
        Assert.NotEmpty(comment.Replies);
        Assert.Equal(comment.Replies.FirstOrDefault()?.Content, newContent);
        Assert.IsType<ReplyUpdatedDomainEvent>(domainEvents.LastOrDefault());
    }

    [Fact]
    public void Delete_Reply_ValidInput_Success()
    {
        Guid postId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        Guid replyUserId = Guid.NewGuid();
        var replyContent = "This is a reply content example";
        var content = "This is a comment content example";

        List<Votes> votes = new();
        List<Replies> replies = new();
        List<RepliesVotes> repliesVotes = new();

        var comment = Comment.Create(
            new UserId(userId),
            new CommunityId(communityId),
            new PostId(postId),
            content,
            votes,
            replies);

        var reply = Replies.Create(
            new UserId(replyUserId),
            new CommunityId(communityId),
            comment.Id,
            replyContent,
            repliesVotes);

        comment.AddReply(reply);

        comment.RemoveReply(reply.Id);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)reply.GetDomainEvents();

        Assert.NotNull(comment.Replies);
        Assert.Empty(comment.Replies);
        Assert.IsType<ReplyDeletedDomainEvent>(domainEvents.LastOrDefault());
    }

    [Fact]
    public void Create_Vote_Reply_ValidInput_Success()
    {
        Guid postId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        Guid replyUserId = Guid.NewGuid();
        Guid voteUserId = Guid.NewGuid();
        var replyContent = "This is a reply content example";
        var content = "This is a comment content example";

        List<Votes> votes = new();
        List<Replies> replies = new();
        List<RepliesVotes> repliesVotes = new();

        var comment = Comment.Create(
            new UserId(userId),
            new CommunityId(communityId),
            new PostId(postId),
            content,
            votes,
            replies);

        var reply = Replies.Create(
            new UserId(replyUserId),
            new CommunityId(communityId),
            comment.Id,
            replyContent,
            repliesVotes);

        comment.AddReply(reply);

        var replyVote = RepliesVotes.Create(
            reply.Id,
            new UserId(voteUserId),
            true);

        reply.AddReplyVote(replyVote);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)replyVote.GetDomainEvents();

        Assert.NotNull(comment.Replies.FirstOrDefault()?.Votes);
        Assert.NotEmpty(comment.Replies.FirstOrDefault()?.Votes!);
        Assert.Equal(comment.Replies.FirstOrDefault()?.Votes.FirstOrDefault(), replyVote);
        Assert.IsType<VoteOnReplyCreatedDomainEvent>(domainEvents.LastOrDefault());
    }

    [Fact]
    public void Update_Vote_Reply_ValidInput_Success()
    {
        Guid postId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        Guid replyUserId = Guid.NewGuid();
        Guid voteUserId = Guid.NewGuid();
        var replyContent = "This is a reply content example";
        var content = "This is a comment content example";

        List<Votes> votes = new();
        List<Replies> replies = new();
        List<RepliesVotes> repliesVotes = new();

        var comment = Comment.Create(
            new UserId(userId),
            new CommunityId(communityId),
            new PostId(postId),
            content,
            votes,
            replies);

        var reply = Replies.Create(
            new UserId(replyUserId),
            new CommunityId(communityId),
            comment.Id,
            replyContent,
            repliesVotes);

        comment.AddReply(reply);

        var replyVote = RepliesVotes.Create(
            reply.Id,
            new UserId(voteUserId),
            true);

        reply.AddReplyVote(replyVote);

        reply.UpdateReplyVote(replyVote.Id, false);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)replyVote.GetDomainEvents();

        Assert.NotNull(comment.Replies.FirstOrDefault()?.Votes);
        Assert.NotEmpty(comment.Replies.FirstOrDefault()?.Votes!);
        Assert.Equal(comment.Replies.FirstOrDefault()?.Votes.FirstOrDefault(), replyVote);
        Assert.False(comment.Replies.FirstOrDefault()?.Votes.FirstOrDefault()?.IsVoted);
        Assert.IsType<VoteOnReplyUpdatedDomainEvent>(domainEvents.LastOrDefault());
    }

    [Fact]
    public void Delete_Vote_Reply_ValidInput_Success()
    {
        Guid postId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Guid userId = Guid.NewGuid();
        Guid replyUserId = Guid.NewGuid();
        Guid voteUserId = Guid.NewGuid();
        var replyContent = "This is a reply content example";
        var content = "This is a comment content example";

        List<Votes> votes = new();
        List<Replies> replies = new();
        List<RepliesVotes> repliesVotes = new();

        var comment = Comment.Create(
            new UserId(userId),
            new CommunityId(communityId),
            new PostId(postId),
            content,
            votes,
            replies);

        var reply = Replies.Create(
            new UserId(replyUserId),
            new CommunityId(communityId),
            comment.Id,
            replyContent,
            repliesVotes);

        comment.AddReply(reply);

        var replyVote = RepliesVotes.Create(
            reply.Id,
            new UserId(voteUserId),
            true);

        reply.AddReplyVote(replyVote);

        reply.RemoveReplyVote(replyVote.Id);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)replyVote.GetDomainEvents();

        Assert.NotNull(comment.Replies.FirstOrDefault()?.Votes);
        Assert.IsType<VoteOnReplyDeletedDomainEvent>(domainEvents.LastOrDefault());
    }
}