## Comment Aggregates

## C# interface
```csharp
    ErrorOr<Comment> GetCommentById(CommentId commentId);
    List<Comment> GetCommentsListByPostId(PostId postId);
    void Add(Comment comment);
    ErrorOr<Comment> UpdateCommentById(CommentId id, UserId userId, string content);
    ErrorOr<bool> DeleteCommentById(CommentId id, UserId userId);

    ErrorOr<bool> AddCommentVote(CommentId id, UserId userId, bool isVoted);
    ErrorOr<bool> UpdateCommentVoteById(CommentId id, VoteId voteId, UserId userId, bool isVoted);
    ErrorOr<bool> DeleteCommentVoteById(CommentId id, VoteId voteId, UserId userId);

    ErrorOr<bool> AddCommentReply(CommentId id, UserId userId, string content);
    ErrorOr<bool> UpdateCommentReplyById(CommentId id, ReplyId replyId, UserId userId, string content);
    ErrorOr<bool> DeleteCommentReplyById(CommentId id, ReplyId replyId, UserId userId);

    ErrorOr<bool> AddReplyVote(CommentId commentId, ReplyId id, UserId userId, bool IsVoted);
    ErrorOr<bool> UpdateReplyVoteById(CommentId id, ReplyId replyId, VoteId voteId, UserId userId, bool isVoted);
    ErrorOr<bool> DeleteReplyVoteById(CommentId id, ReplyId replyId, VoteId voteId, UserId userId);

    ErrorOr<List<RepliesVotes>> GetVoteListByReplyId(CommentId commentId, ReplyId replyId);
    ErrorOr<List<Replies>> GetReplyListByCommentId(CommentId commentId);
    ErrorOr<List<Votes>> GetVoteListByCommentId(CommentId commentId);

    bool UserExists(UserId userId);
    bool CommunityExists(CommunityId communityId);
    bool PostExists(PostId postId);
    bool CommentExists(CommentId commentId);
    bool CommentVoteExists(CommentId commentId, VoteId voteId);
    bool UserAlreadyVoted(CommentId commentId, UserId userId);
    bool CommentReplyExists(CommentId commentId, ReplyId replyId);
```

## Domain object representation
```json
{
    "id": { "value": "000000000-0000-0000-000000" },
    "postId": { "value": "000000000-0000-0000-000000" },
    "userId": { "value": "000000000-0000-0000-000000" },
    "content": "Hello i am a content",
    "votes": [
        {
            "id": { "value": "000000000-0000-0000-000000" },
            "userId": { "value": "000000000-0000-0000-000000" },
            "commentId": { "value": "000000000-0000-0000-000000" },
            "isVoted": true
        }],
    "replies": [
        {
            "id": { "value": "000000000-0000-0000-000000"},
            "userId":  { "value": "000000000-0000-0000-000000" },
            "commentId":  { "value": "000000000-0000-0000-000000" },
            "content": "some content",
            "votes": [
                {
                    "id": { "value": "000000000-0000-0000-000000" },
                    "userId": { "value": "000000000-0000-0000-000000" },
                    "replyId": { "value": "000000000-0000-0000-000000" },
                    "isVoted": true
                }],
        }
    ],
    "createdAt": "2020-01-01T00:00:00.00000000Z",
    "updatedAt": "2020-01-01T00:00:00.00000000Z"
}
```