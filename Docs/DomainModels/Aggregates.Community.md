## Community Aggregates

## C# interface
```csharp
    ErrorOr<Community> GetCommunityById(CommunityId communityId);
    ErrorOr<Community> GetCommunityByName(string name);
    List<Community> GetCommunitiesList();
    void Add(Community community);
    ErrorOr<bool> UpdateCommunityById(CommunityId id, UserId userId, string name, string description, string topic);
    ErrorOr<bool> DeleteCommunityById(CommunityId id, UserId userId);
    bool UserExists(UserId userId);
```

## Domain object representation
```json
{
    "id": { "value": "000000000-0000-0000-000000"},
    "name": "C#",
    "description": "hello i am a description",
    "membersCount": 0,
    "topic": "Programming",
    "createdAt": "2020-01-01T00:00:00.00000000Z",
    "updatedAt": "2020-01-01T00:00:00.00000000Z"
}
```