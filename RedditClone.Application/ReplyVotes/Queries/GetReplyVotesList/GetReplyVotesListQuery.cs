namespace RedditClone.Application.ReplyVotes.Queries.GetRepliesVotesList;

using MediatR;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.ReplyVotes.Results.GetReplyVotesListResults;

public record GetReplyVotesListQuery(
    CommentId CommentId,
    ReplyId ReplyId
) : IRequest<GetReplyVotesListResult>;

