using MediatR;
using RedditClone.Application.Comment.Results.CreateCommentResult;

namespace RedditClone.Application.Comment.Commands.CreateCommentCommand;

public record CreateCommentCommand(
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt)
: IRequest<CreateCommentResult>;