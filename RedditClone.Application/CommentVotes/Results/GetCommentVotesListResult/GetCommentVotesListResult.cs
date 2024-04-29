namespace RedditClone.Application.CommentVotes.Results.GetCommentVotesListResult;

using RedditClone.Domain.CommentAggregate.Entities;

public record GetCommentVotesListResult(
    List<Votes> Votes
);