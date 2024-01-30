using ErrorOr;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.Entities;

namespace RedditClone.Application.Comment.Commands.CreateCommentCommand;

public class CreateCommentCommandHandler :
    IRequestHandler<CreateCommentCommand,
    ErrorOr<CommentAggregate>>
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<ErrorOr<CommentAggregate>> Handle(CreateCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        //create comment
        var comment = CommentAggregate.Create(
            command.Content,
            command.UserId,
            command.PostId,
            command.CreatedAt,
            command.UpdatedAt,
            command.Replies,
            command.Upvotes,
            command.Downvotes
        );

        //persist comment
        _commentRepository.Add(comment);

        //return comment
        return comment;
    }
}