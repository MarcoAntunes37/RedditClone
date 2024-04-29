namespace RedditClone.Application.Comment.Commands.CreateComment;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Application.Persistence;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Comment.Results.CreateCommentResult;

public class CreateCommentCommandHandler(
    ICommentRepository commentRepository,
    IPostRepository postRepository,
    IUserCommunitiesRepository userCommunitiesRepository)
: IRequestHandler<CreateCommentCommand, ErrorOr<CreateCommentResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;
    private readonly IPostRepository postRepository = postRepository;
    private readonly IUserCommunitiesRepository _userCommunitiesRepository = userCommunitiesRepository;

    public async Task<ErrorOr<CreateCommentResult>> Handle(
        CreateCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@CreateCommentCommand}",
            "Trying to create comment in Post: {@PostId}",
            command,
            command.PostId);

        var userCommunity = _userCommunitiesRepository.GetUserCommunities(command.UserId, command.CommunityId);

        if (userCommunity is null)
        {
            Error error = Errors.UserCommunities.UserNotInCommunity;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        if (!postRepository.UserExists(command.UserId))
        {
            Error error = Errors.User.UserNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        if (!postRepository.CommunityExists(command.CommunityId))
        {
            Error error = Errors.Community.CommunityNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        if(!_commentRepository.PostExists(command.PostId))
        {
            Error error = Errors.Posts.PostNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        var comment = Comment.Create(
            command.UserId,
            command.CommunityId,
            command.PostId,
            command.Content,
            command.Votes,
            command.Replies
        );

        _commentRepository.Add(comment);

        CreateCommentResult result = new(comment);

        Log.Information(
            "{@Message}, {@CreateCommentResult}",
            "Comment created successfully",
            result);

        return result;
    }
}