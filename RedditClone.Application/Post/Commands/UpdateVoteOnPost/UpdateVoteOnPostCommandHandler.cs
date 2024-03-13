namespace RedditClone.Application.Post.Commands.UpdateVoteOnPost;

using FluentValidation;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.UpdateVoteOnPostResult;

public class UpdateVoteOnPostCommandHandler
    : IRequestHandler<UpdateVoteOnPostCommand, UpdateVoteOnPostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<UpdateVoteOnPostCommand> _validator;

    public UpdateVoteOnPostCommandHandler(
        IPostRepository postRepository,
        IValidator<UpdateVoteOnPostCommand> validator)
    {
        _postRepository = postRepository;
        _validator = validator;
    }

    public async Task<UpdateVoteOnPostResult> Handle(UpdateVoteOnPostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _postRepository.UpdatePostVoteById(command.PostId, command.VoteId, command.UserId, command.IsVoted);

        return new UpdateVoteOnPostResult(
            "Vote on post updated successfully"
        );
    }
}