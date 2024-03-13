namespace RedditClone.Application.Comment.Commands.ReplyOnComment;

using FluentValidation;
using MediatR;
using RedditClone.Application.Comment.Results.ReplyOnCommentResult;
using RedditClone.Application.Common.Interfaces.Persistence;
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
            throw new Exception("You need to join into community to reply");

        _validator.ValidateAndThrow(command);

        _commentRepository.AddCommentReply(command.CommentId, command.UserId, command.CommunityId, command.Content);

        return new ReplyOnCommentResult(
            "Comment replied successfully"
        );
    }
}