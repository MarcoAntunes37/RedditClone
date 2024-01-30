using ErrorOr;
using MediatR;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommentAggregate.ValueObjects;

namespace RedditClone.Application.Comment.Commands.CreateCommentCommand;

public record CreateCommentCommand(
    UserId UserId,
    PostId PostId,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<Replies> Replies,
    List<Upvotes> Upvotes,
    List<Downvotes> Downvotes) : IRequest<ErrorOr<CommentAggregate>>;