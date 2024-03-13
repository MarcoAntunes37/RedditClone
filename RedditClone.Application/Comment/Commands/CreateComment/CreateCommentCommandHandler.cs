namespace RedditClone.Application.Comment.Commands.CreateComment;

using FluentValidation;
using MediatR;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;

public class CreateCommentCommandHandler :
    IRequestHandler<CreateCommentCommand, CreateCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserCommunitiesRepository _userCommunitiesRepository;
    private readonly IValidator<CreateCommentCommand> _validator;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        IUserCommunitiesRepository userCommunitiesRepository,
        IValidator<CreateCommentCommand> validator)
    {
        _commentRepository = commentRepository;
        _userCommunitiesRepository = userCommunitiesRepository;
        _validator = validator;
    }

    public async Task<CreateCommentResult> Handle(CreateCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        bool isValid = _userCommunitiesRepository.ValidateRelationship(command.UserId, command.CommunityId);

        if(!isValid)
            throw new Exception("You need to join into community to comment");

        _validator.ValidateAndThrow(command);

        var comment = Comment.Create(
            command.UserId,
            command.CommunityId,
            command.PostId,
            command.Content,
            command.CreatedAt,
            command.UpdatedAt,
            command.Votes,
            command.Replies
        );

        _commentRepository.Add(comment);

        return new CreateCommentResult(
            comment
        );
    }
}