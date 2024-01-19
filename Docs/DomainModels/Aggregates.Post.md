## Post
```csharp
    Post Create();
    void Update(Post post);
    void Delete(Guid postId);
    void Upvote(Guid postId, Guid userId);
    void Downvote(Guid postId, Guid userId);
```

```json
{
    "id": { "value": "000000000-0000-0000-000000"},
    "userId": { "value": "000000000-0000-0000-000000"},
    "communityId": { "value": "000000000-0000-0000-000000"},
    "title": "Post title",
    "content": "Post body",
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