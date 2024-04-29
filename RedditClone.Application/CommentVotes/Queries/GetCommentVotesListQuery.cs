namespace RedditClone.Application.CommentVotes.Queries;

using MediatR;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentVotes.Results.GetCommentVotesListResult;

public record GetCommentVotesListQuery(
    CommentId CommentId
): IRequest<GetCommentVotesListResult>;