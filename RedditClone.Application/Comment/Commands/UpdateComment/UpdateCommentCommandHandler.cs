namespace RedditClone.Application.Community.Commands.UpdateComment;

using FluentValidation;
using MediatR;
using RedditClone.Application.Comment.Commands.UpdateComment;
using RedditClone.Application.Community.Results.UpdateCommentResult;
using RedditClone.Application.Persistence;

public class UpdateCommentCommandHandler :
    IRequestHandler<UpdateCommentCommand, UpdateCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<UpdateCommentCommand> _validator;

    public UpdateCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<UpdateCommentCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }

    public async Task<UpdateCommentResult> Handle(UpdateCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);


        return new UpdateCommentResult(
            "Comment successfully updated."
        );
    }
}