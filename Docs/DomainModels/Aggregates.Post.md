## Post

## C# interface
```csharp
    ErrorOr<Post> GetPostById(PostId postId);
    List<Post> GetPostListByUser(UserId userId);
    List<Votes> GetVotesListsByPostId(PostId postId);
    List<Post> GetPostListByCommunity(CommunityId communityId);
    void Add(Post post);
    Post UpdatePostById(PostId id, string title, string content);
    ErrorOr<bool> DeletePostById(PostId id, UserId userId);
    ErrorOr<bool> AddPostVote(PostId id, UserId userId, bool isVoted);
    ErrorOr<bool> UpdatePostVoteById(PostId id, VoteId voteId, UserId userId, bool isVoted);
    ErrorOr<bool> DeletePostVoteById(PostId id, VoteId voteId, UserId userId);
    bool UserExists(UserId userId);
    bool CommunityExists(CommunityId communityId);
```

## Domain object representation
```json
{
    "id": { "value": "000000000-0000-0000-000000" },
    "userId": { "value": "000000000-0000-0000-000000" },
    "communityId": { "value": "000000000-0000-0000-000000" },
    "title": "Post title",
    "content": "Post body",
    "votes": [
        {
            "id": { "value": "000000000-0000-0000-000000" },
            "userId": { "value": "000000000-0000-0000-000000" },
            "postId": { "value": "000000000-0000-0000-000000" },
            "isVoted": true
        }],
    "createdAt": "2020-01-01T00:00:00.00000000Z",
    "updatedAt": "2020-01-01T00:00:00.00000000Z"
}
```