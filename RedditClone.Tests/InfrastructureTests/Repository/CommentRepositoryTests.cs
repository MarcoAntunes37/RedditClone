namespace RedditClone.Tests.InfrastructureTests.Repository;

using System;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;

public class CommentRepositoryTests
{
    [Fact]
    public void CommentRepository_ShouldAddComment_WhenCommentIsValid()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var comment = arrange["comment"] as Comment;

        using (var context = new RedditCloneDbContext(options!))
        {
            var commentRepository = new CommentRepository(context);

            commentRepository.Add(comment!);

            context.SaveChanges();

            Assert.Equal(1, context.Comments.Count());

            Assert.Equal(arrange["content"].ToString(), comment!.Content);
        }
    }

    [Fact]
    public void CommentRepository_ShouldUpdateComment_WhenCommentExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var comment = arrange["comment"] as Comment;

        using (var context = new RedditCloneDbContext(options!))
        {
            var commentRepository = new CommentRepository(context);

            commentRepository.Add(comment!);

            context.SaveChanges();

            commentRepository.UpdateCommentById(comment!.Id, comment.UserId, arrange["newContent"].ToString()!);

            context.SaveChanges();

            Assert.Equal(1, context.Comments.Count());

            Assert.Equal(arrange["newContent"].ToString(), comment!.Content);
        }
    }

    [Fact]
    public void CommentRepository_ShouldDeleteComment_WhenCommentExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var comment = arrange["comment"] as Comment;

        using (var context = new RedditCloneDbContext(options!))
        {
            var commentRepository = new CommentRepository(context);

            commentRepository.Add(comment!);

            context.SaveChanges();

            commentRepository.DeleteCommentById(comment!.Id, comment.UserId);

            context.SaveChanges();

            Assert.Equal(0, context.Comments.Count());
        }
    }

    [Fact]
    public void CommentRepository_ShouldAddVoteOnComment_WhenCommentExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var comment = arrange["comment"] as Comment;

        using (var context = new RedditCloneDbContext(options!))
        {
            var commentRepository = new CommentRepository(context);

            commentRepository.Add(comment!);

            context.SaveChanges();

            commentRepository.AddCommentVote(comment!.Id, comment!.UserId, true);

            context.SaveChanges();

            Assert.Single(comment!.Votes);
        }
    }

    [Fact]
    public void CommentRepository_ShouldUpdateVoteOnComment_WhenCommentExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var comment = arrange["comment"] as Comment;

        using (var context = new RedditCloneDbContext(options!))
        {
            var commentRepository = new CommentRepository(context);

            commentRepository.Add(comment!);

            context.SaveChanges();

            commentRepository.AddCommentVote(comment!.Id, comment!.UserId, true);

            context.SaveChanges();

            var vote = comment.Votes.First();

            commentRepository.UpdateCommentVoteById(comment.Id, vote.Id, vote.UserId, false);

            context.SaveChanges();

            Assert.False(vote.IsVoted);
        }
    }

    [Fact]
    public void CommentRepository_ShouldDeleteVoteOnComment_WhenCommentExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var comment = arrange["comment"] as Comment;

        using (var context = new RedditCloneDbContext(options!))
        {
            var commentRepository = new CommentRepository(context);

            commentRepository.Add(comment!);

            context.SaveChanges();

            commentRepository.AddCommentVote(comment!.Id, comment!.UserId, true);

            context.SaveChanges();

            var vote = comment.Votes.First();

            commentRepository.DeleteCommentVoteById(comment.Id, vote.Id, vote.UserId);

            context.SaveChanges();

            Assert.Empty(comment.Votes);
        }
    }
    [Fact]
    public void CommentRepository_ShouldAddReplyOnComment_WhenCommentExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var comment = arrange["comment"] as Comment;

        using (var context = new RedditCloneDbContext(options!))
        {
            var commentRepository = new CommentRepository(context);

            commentRepository.Add(comment!);

            context.SaveChanges();

            commentRepository.AddCommentReply(comment!.Id, comment!.UserId, arrange["content"].ToString()!);

            context.SaveChanges();

            Assert.Single(comment!.Replies);
        }
    }

    [Fact]
    public void CommentRepository_ShouldUpdateReplyOnComment_WhenCommentExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var comment = arrange["comment"] as Comment;


        using (var context = new RedditCloneDbContext(options!))
        {
            var commentRepository = new CommentRepository(context);

            commentRepository.Add(comment!);

            context.SaveChanges();

            commentRepository.AddCommentReply(comment!.Id, comment!.UserId, arrange["content"].ToString()!);

            context.SaveChanges();

            var reply = comment.Replies.First();

            commentRepository.UpdateCommentReplyById(comment.Id, reply.Id, reply.UserId, arrange["newContent"].ToString()!);

            context.SaveChanges();

            Assert.Equal(arrange["newContent"].ToString(), reply.Content);
        }
    }

    [Fact]
    public void CommentRepository_ShouldDeleteReplyOnComment_WhenCommentExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var comment = arrange["comment"] as Comment;

        using (var context = new RedditCloneDbContext(options!))
        {
            var commentRepository = new CommentRepository(context);

            commentRepository.Add(comment!);

            context.SaveChanges();

            commentRepository.AddCommentReply(comment!.Id, comment!.UserId, arrange["content"].ToString()!);

            context.SaveChanges();

            var reply = comment.Replies.First();

            commentRepository.DeleteCommentReplyById(comment.Id, reply.Id, reply.UserId);

            context.SaveChanges();

            Assert.Empty(comment.Replies);
        }
    }

    [Fact]
    public void CommentRepository_ShouldAddVoteOnReply_WhenReplyExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var comment = arrange["comment"] as Comment;

        using (var context = new RedditCloneDbContext(options!))
        {
            var commentRepository = new CommentRepository(context);

            commentRepository.Add(comment!);

            context.SaveChanges();

            commentRepository.AddCommentReply(comment!.Id, comment!.UserId, arrange["content"].ToString()!);

            context.SaveChanges();

            var reply = comment.Replies.First();

            commentRepository.AddReplyVote(comment.Id, reply.Id, reply.UserId, true);

            context.SaveChanges();

            Assert.True(reply.Votes.First().IsVoted);

            Assert.Single(reply.Votes);
        }
    }

    [Fact]
    public void CommentRepository_ShouldUpdateVoteOnReply_WhenReplyExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var comment = arrange["comment"] as Comment;

        using (var context = new RedditCloneDbContext(options!))
        {
            var commentRepository = new CommentRepository(context);

            commentRepository.Add(comment!);

            context.SaveChanges();

            commentRepository.AddCommentReply(comment!.Id, comment!.UserId, arrange["content"].ToString()!);

            context.SaveChanges();

            var reply = comment.Replies.First();

            commentRepository.AddReplyVote(comment.Id, reply.Id, reply.UserId, true);

            context.SaveChanges();

            var vote = reply.Votes.First();

            commentRepository.UpdateReplyVoteById(comment.Id, reply.Id, vote.Id, vote.UserId, false);

            context.SaveChanges();

            Assert.False(vote.IsVoted);
        }
    }

    [Fact]
    public void CommentRepository_ShouldDeleteVoteOnReply_WhenReplyExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var comment = arrange["comment"] as Comment;

        using (var context = new RedditCloneDbContext(options!))
        {
            var commentRepository = new CommentRepository(context);

            commentRepository.Add(comment!);

            context.SaveChanges();

            commentRepository.AddCommentReply(comment!.Id, comment!.UserId, arrange["content"].ToString()!);

            context.SaveChanges();

            var reply = comment.Replies.First();

            commentRepository.AddReplyVote(comment.Id, reply.Id, reply.UserId, true);

            context.SaveChanges();

            var vote = reply.Votes.First();

            commentRepository.DeleteReplyVoteById(comment.Id, reply.Id, vote.Id, vote.UserId);

            context.SaveChanges();

            Assert.Empty(reply.Votes);
        }
    }

    private static Dictionary<string, object> CreateTestArranges()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        List<Votes> votes = new();

        List<Replies> replies = new();

        var content = "TestComment";

        var newContent = "UpdatedComment";

        var comment = Comment.Create(
            new UserId(Guid.NewGuid()),
            new CommunityId(Guid.NewGuid()),
            new PostId(Guid.NewGuid()),
            content,
            votes,
            replies);

        return new Dictionary<string, object>()
        {
            { "comment", comment },
            { "options", options },
            { "content", content },
            { "newContent", newContent }

        };
    }
}