namespace RedditClone.Application.Post.Commands.UpdatePost;

using FluentValidation;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.UpdatePostResult;

public class UpdatePostCommandHandler
    : IRequestHandler<UpdatePostCommand, UpdatePostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<UpdatePostCommand> _validator;

    public UpdatePostCommandHandler(
        IPostRepository postRepository,
        IValidator<UpdatePostCommand> validator)
    {
        _postRepository = postRepository;
        _validator = validator;
    }

    public async Task<UpdatePostResult> Handle(UpdatePostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _postRepository.UpdatePostById(command.PostId, command.UserId, command.Title, command.Content);

        return new UpdatePostResult(
            "Post updated successfully"
        );
    }
}