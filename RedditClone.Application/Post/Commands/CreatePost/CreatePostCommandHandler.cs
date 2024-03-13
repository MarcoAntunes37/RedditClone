namespace RedditClone.Application.Post.Commands.CreatePost;

using FluentValidation;
using MediatR;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.CreatePostResult;
using RedditClone.Domain.PostAggregate;

public class CreatePostCommandHandler
    : IRequestHandler<CreatePostCommand, CreatePostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserCommunitiesRepository _userCommunitiesRepository;

    private readonly IValidator<CreatePostCommand> _validator;

    public CreatePostCommandHandler(
        IPostRepository postRepository,
        IUserCommunitiesRepository userCommunitiesRepository,
        IValidator<CreatePostCommand> validator)
    {
        _postRepository = postRepository;
        _userCommunitiesRepository = userCommunitiesRepository;
        _validator = validator;
    }

    public async Task<CreatePostResult> Handle(CreatePostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        bool isValid = _userCommunitiesRepository.ValidateRelationship(command.UserId, command.CommunityId);

        if(!isValid)
            throw new Exception("You need to join into community to post");

        _validator.ValidateAndThrow(command);

        var post = Post.Create(
            command.CommunityId,
            command.UserId,
            command.Title,
            command.Content,
            command.CreatedAt,
            command.UpdatedAt,
            command.Votes
        );

        _postRepository.Add(post);

        return new CreatePostResult(
            post
        );
    }
}