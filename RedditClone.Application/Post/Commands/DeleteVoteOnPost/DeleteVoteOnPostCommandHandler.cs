namespace RedditClone.Application.Post.Commands.DeleteVoteOnPost;

using FluentValidation;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.DeleteVoteOnPostResult;

public class DeleteVoteOnPostCommandHandler
    : IRequestHandler<DeleteVoteOnPostCommand, DeleteVoteOnPostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<DeleteVoteOnPostCommand> _validator;

    public DeleteVoteOnPostCommandHandler(
        IPostRepository postRepository,
        IValidator<DeleteVoteOnPostCommand> validator)
    {
        _postRepository = postRepository;
        _validator = validator;
    }

    public async Task<DeleteVoteOnPostResult> Handle(DeleteVoteOnPostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _postRepository.DeletePostVoteById(command.PostId, command.VoteId, command.UserId);

        return new DeleteVoteOnPostResult(
            "Vote on post deleted successfully"
        );
    }
}