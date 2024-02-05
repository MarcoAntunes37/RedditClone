using FluentValidation;
using MediatR;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;

namespace RedditClone.Application.Comment.Commands.CreateCommentCommand;

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

        //create comment
        var comment = CommentAggregate.Create(
            command.Content,
            command.CreatedAt,
            command.UpdatedAt
        );

        //persist comment
        _commentRepository.Add(comment);

        //return comment
        return new CreateCommentResult(
            comment
        );
    }
}