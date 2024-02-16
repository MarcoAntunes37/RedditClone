namespace RedditClone.Application.Comment.Commands.CreateCommentCommand;

using FluentValidation;
using MediatR;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;

public class CreateCommentCommandHandler :
    IRequestHandler<CreateCommentCommand, CreateCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<CreateCommentCommand> _validator;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<CreateCommentCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }

    public async Task<CreateCommentResult> Handle(CreateCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        var comment = Comment.Create(
            command.UserId,
            command.PostId,
            command.Content,
            command.CreatedAt,
            command.UpdatedAt,
            command.Votes,
            command.Replies
        );

        _commentRepository.Add(comment);

        return new CreateCommentResult(
            comment
        );
    }
}