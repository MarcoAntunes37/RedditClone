namespace RedditClone.Application.CommentReplies.Commands.CreateCommentReply;

using MediatR;
using ErrorOr;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.CommentReplies.Results.CreateCommentReplyResults;

public class CreateCommentReplyCommandHandler(
    ICommentRepository commentRepository,
    IUserCommunitiesRepository userCommunitiesRepository) :
    IRequestHandler<CreateCommentReplyCommand, ErrorOr<CreateCommentReplyResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;
    private readonly IUserCommunitiesRepository _userCommunitiesRepository = userCommunitiesRepository;

    public async Task<ErrorOr<CreateCommentReplyResult>> Handle(
        CreateCommentReplyCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var comment = _commentRepository.GetCommentById(command.CommentId).Value;

        if(comment is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        var userCommunity = _userCommunitiesRepository.GetUserCommunities(command.UserId, command.CommunityId);

        if(userCommunity is null)
        {
            Error error = Errors.UserCommunities.UserNotInCommunity;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        _commentRepository.AddCommentReply(command.CommentId, command.UserId, command.CommunityId, command.Content);

        CreateCommentReplyResult result = new ("Comment replied successfully");

        Log.Information(
            "{@CreateCommentReplyResult}",
            result);

        return result;
    }
}