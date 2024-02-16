namespace RedditClone.Application.Post.Commands.DeletePost;

using FluentValidation;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.DeletePostResult;

public class DeletePostCommandHandler
    : IRequestHandler<DeletePostCommand, DeletePostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<DeletePostCommand> _validator;

    public DeletePostCommandHandler(
        IPostRepository postRepository,
        IValidator<DeletePostCommand> validator)
    {
        _postRepository = postRepository;
        _validator = validator;
    }

    public async Task<DeletePostResult> Handle(DeletePostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _postRepository.DeletePostById(command.PostId, command.UserId);

        return new DeletePostResult(
            "Post deleted successfully"
        );
    }
}