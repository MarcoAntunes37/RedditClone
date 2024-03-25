namespace RedditClone.Application.Comment.Commands.UpdateVoteOnReply;

using FluentValidation;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Comment.Results.UpdateVoteOnReplyResult;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;

public class UpdateVoteOnReplyCommandHandler
    : IRequestHandler<UpdateVoteOnReplyCommand, UpdateVoteOnReplyResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<UpdateVoteOnReplyCommand> _validator;
    private readonly IConfiguration _configuration;

    public UpdateVoteOnReplyCommandHandler(
        ICommentRepository commentRepository,
        IValidator<UpdateVoteOnReplyCommand> validator,
        IConfiguration configuration)
    {
        _commentRepository = commentRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<UpdateVoteOnReplyResult> Handle(UpdateVoteOnReplyCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        _validator.ValidateAndThrow(command);

        _commentRepository.UpdateReplyVoteById(command.CommentId, command.ReplyId, command.VoteId, command.UserId, command.IsVoted);

        UpdateVoteOnReplyResult result = new("Comment reply vote updated successfully");

        return result;
    }
}