namespace RedditClone.Application.Comment.Commands.ReplyOnComment;

using System.Net;
using FluentValidation;
using MediatR;
using RedditClone.Application.Comment.Results.ReplyOnCommentResult;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Errors;
using RedditClone.Application.Persistence;

public class ReplyOnCommentCommandHandler :
    IRequestHandler<ReplyOnCommentCommand, ReplyOnCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserCommunitiesRepository _userCommunitiesRepository;
    private readonly IValidator<ReplyOnCommentCommand> _validator;

    public ReplyOnCommentCommandHandler(
        ICommentRepository commentRepository,
        IUserCommunitiesRepository userCommunitiesRepository,
        IValidator<ReplyOnCommentCommand> validator)
    {
        _commentRepository = commentRepository;
        _userCommunitiesRepository = userCommunitiesRepository;
        _validator = validator;
    }

    public async Task<ReplyOnCommentResult> Handle(ReplyOnCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        bool isValid = _userCommunitiesRepository.ValidateRelationship(command.UserId, command.CommunityId);

        if(!isValid)
            throw new HttpCustomException(
            HttpStatusCode.NotFound, "User is not part of community");

        _validator.ValidateAndThrow(command);

        _commentRepository.AddCommentReply(command.CommentId, command.UserId, command.CommunityId, command.Content);

        return new ReplyOnCommentResult(
            "Comment replied successfully"
        );
    }
}