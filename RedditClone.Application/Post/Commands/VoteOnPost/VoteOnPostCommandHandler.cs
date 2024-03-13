namespace RedditClone.Application.Post.Commands.VoteOnPost;

using FluentValidation;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.VoteOnPostResult;

public class VoteOnPostCommandHandler
    : IRequestHandler<VoteOnPostCommand, VoteOnPostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<VoteOnPostCommand> _validator;

    public VoteOnPostCommandHandler(
        IPostRepository postRepository,
        IValidator<VoteOnPostCommand> validator)
    {
        _postRepository = postRepository;
        _validator = validator;
    }

    public async Task<VoteOnPostResult> Handle(VoteOnPostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _postRepository.AddPostVote(command.PostId, command.UserId, command.IsVoted);

        return new VoteOnPostResult(
            "Vote on post successfully"
        );
    }
}