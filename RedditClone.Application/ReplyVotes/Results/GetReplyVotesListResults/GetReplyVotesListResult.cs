namespace RedditClone.Application.ReplyVotes.Results.GetReplyVotesListResults;

using RedditClone.Domain.CommentAggregate.Entities;

public record GetReplyVotesListResult(
    List<RepliesVotes> ReplyVotes
);