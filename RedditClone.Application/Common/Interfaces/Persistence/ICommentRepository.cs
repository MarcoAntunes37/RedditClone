namespace RedditClone.Application.Persistence;

using ErrorOr;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public interface ICommentRepository
{
    ErrorOr<Comment> GetCommentById(CommentId commentId);
    List<Comment> GetCommentsListByPostId(PostId postId);
    void Add(Comment comment);
    ErrorOr<Comment> UpdateCommentById(CommentId id, UserId userId, string content);
    ErrorOr<bool> DeleteCommentById(CommentId id, UserId userId);

    ErrorOr<bool> AddCommentVote(CommentId id, UserId userId, bool isVoted);
    ErrorOr<bool> UpdateCommentVoteById(CommentId id, VoteId voteId, UserId userId, bool isVoted);
    ErrorOr<bool> DeleteCommentVoteById(CommentId id, VoteId voteId, UserId userId);

    ErrorOr<bool> AddCommentReply(CommentId id, UserId userId, CommunityId communityId, string content);
    ErrorOr<bool> UpdateCommentReplyById(CommentId id, ReplyId replyId, UserId userId, string content);
    ErrorOr<bool> DeleteCommentReplyById(CommentId id, ReplyId replyId, UserId userId);

    ErrorOr<bool> AddReplyVote(CommentId commentId, ReplyId id, UserId userId, bool IsVoted);
    ErrorOr<bool> UpdateReplyVoteById(CommentId id, ReplyId replyId, VoteId voteId, UserId userId, bool isVoted);
    ErrorOr<bool> DeleteReplyVoteById(CommentId id, ReplyId replyId, VoteId voteId, UserId userId);
}