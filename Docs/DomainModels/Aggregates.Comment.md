## Comment Aggregates
```csharp
    Comment Create();
    void Update(Comment comment);
    void Delete(Guid commentId);
    void Upvote(Guid commentId, Guid userId);
    void Downvote(Guid commentId, Guid userId);
```

```json
{
    "id": { "value": "000000000-0000-0000-000000"},
    "postId": { "value": "000000000-0000-0000-000000"},
    "userId": "marcodev",
    "content": "Hello i am a content",
    "replies": [
        {
            "id": { "value": "000000000-0000-0000-000000"},
            "userId":  { "value": "000000000-0000-0000-000000" },
            "username": "other",
            "content": "some content",
            "upvotes": [
                {
                    "id": { "value": "000000000-0000-0000-000000"},
                    "userId": { "value": "000000000-0000-0000-000000"}
                }
            ],
            "downvotes": [
                {
                    "id": { "value": "000000000-0000-0000-000000"},
                    "userId": { "value": "000000000-0000-0000-000000"}
                }
            ]
        }
    ],
    "upvotes": [
        {
            "id": { "value": "000000000-0000-0000-000000"},
            "userId": { "value": "000000000-0000-0000-000000"}
        }
    ],
    "downvotes": [
        {
            "id": { "value": "000000000-0000-0000-000000"},
            "userId": { "value": "000000000-0000-0000-000000"}
        }
    ],
    "createdAt": "2020-01-01T00:00:00.00000000Z",
    "updatedAt": "2020-01-01T00:00:00.00000000Z"
}
```