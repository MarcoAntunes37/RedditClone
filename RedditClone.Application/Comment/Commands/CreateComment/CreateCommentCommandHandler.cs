namespace RedditClone.Application.Comment.Commands.CreateComment;

using System.Net;
using FluentValidation;
using MediatR;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using Serilog;

public class CreateCommentCommandHandler :
    IRequestHandler<CreateCommentCommand, CreateCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserCommunitiesRepository _userCommunitiesRepository;
    private readonly IValidator<CreateCommentCommand> _validator;
    private readonly IConfiguration _configuration;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        IUserCommunitiesRepository userCommunitiesRepository,
        IValidator<CreateCommentCommand> validator,
        IConfiguration configuration)
    {
        _commentRepository = commentRepository;
        _userCommunitiesRepository = userCommunitiesRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<CreateCommentResult> Handle(CreateCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@CreateCommentCommand}",
            "Trying to create comment in Post: {@PostId}",
            command,
            command.PostId);

        bool isValid = _userCommunitiesRepository.ValidateRelationship(command.UserId, command.CommunityId);

        if(!isValid)
            throw new HttpCustomException(HttpStatusCode.NotFound, "User is not part of community");

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

        CreateCommentResult result = new(comment);

        Log.Information(
            "{@Message}, {@CreateCommentResult}",
            "Comment created successfully",
            result);

        return result;
    }
}