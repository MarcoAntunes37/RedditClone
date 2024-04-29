namespace RedditClone.Application.CommentReplies.Queries.GetCommentRepliesList;

using MediatR;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentReplies.Results.GetCommentRepliesListResult;

public record GetCommentRepliesListQuery(
    CommentId CommentId,
    int Page,
    int PageSize)
: IRequest<GetCommentRepliesListResult>;