namespace RedditClone.Application.Post.Commands.CreatePost;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Post.Results.CreatePostResult;

public class CreatePostCommandHandler(
    IPostRepository postRepository,
    IUserCommunitiesRepository userCommunitiesRepository)
        : IRequestHandler<CreatePostCommand, ErrorOr<CreatePostResult>>
{
    private readonly IPostRepository _postRepository = postRepository;
    private readonly IUserCommunitiesRepository _userCommunitiesRepository = userCommunitiesRepository;

    public async Task<ErrorOr<CreatePostResult>> Handle(CreatePostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@CreatePostCommand}",
            "Trying to create post in community {@CommunityId}",
            command,
            command.CommunityId);

        if(_userCommunitiesRepository.GetUserCommunities(command.UserId, command.CommunityId) == null)
        {
            Error error = Errors.UserCommunities.UserNotInCommunity;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        };

        var post = Post.Create(
            command.CommunityId,
            command.UserId,
            command.Title,
            command.Content,
            command.Votes
        );

        _postRepository.Add(post);

        CreatePostResult result = new(post);

        Log.Information(
            "{@Message}, {@CreatePostResult}",
            "Post created successfully",
            result);

        return result;
    }
}