## Community Aggregates

## C# interface
```csharp
    void Create(Community community);
    void Update(Community community);
    void Delete(Guid communityId);
```

## Domain object representation
```json
{
    "id": { "value": "000000000-0000-0000-000000"},
    "userId": { "value": "000000000-0000-0000-000000"},
    "name": "C#",
    "description": "hello i am a description",
    "membersCount": 0,
    "topic": "Programming",
    "createdAt": "2020-01-01T00:00:00.00000000Z",
    "updatedAt": "2020-01-01T00:00:00.00000000Z"
}
```