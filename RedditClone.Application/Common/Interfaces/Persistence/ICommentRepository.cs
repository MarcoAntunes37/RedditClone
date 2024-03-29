namespace RedditClone.Application.Persistence;

using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public interface ICommentRepository
{
    Comment GetCommentById(CommentId commentId);
    List<Comment> GetCommentsListByPostId(PostId postId);
    void Add(Comment comment);
    Comment UpdateCommentById(CommentId id, UserId userId, string content);
    void DeleteCommentById(CommentId id, UserId userId);

    void AddCommentVote(CommentId id, UserId userId, bool isVoted);
    void UpdateCommentVoteById(CommentId id, VoteId voteId, UserId userId, bool isVoted);
    void DeleteCommentVoteById(CommentId id, VoteId voteId, UserId userId);

    void AddCommentReply(CommentId id, UserId userId, CommunityId communityId, string content);
    void UpdateCommentReplyById(CommentId id, ReplyId replyId, UserId userId, string content);
    void DeleteCommentReplyById(CommentId id, ReplyId replyId, UserId userId);

    void AddReplyVote(CommentId commentId, ReplyId id, UserId userId, bool IsVoted);
    void UpdateReplyVoteById(CommentId id, ReplyId replyId, VoteId voteId, UserId userId, bool isVoted);
    void DeleteReplyVoteById(CommentId id, ReplyId replyId, VoteId voteId, UserId userId);
}