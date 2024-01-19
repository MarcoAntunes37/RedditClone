## User Aggregates

```csharp
    void Register(User user);
    void Login(string username, string password);
    void Update(User user);
    void SubscribeCommunity(Guid communityid);
    void Delete(Guid userId);
```

```json
{
    "id": { "value": "000000000-0000-0000-000000" },
    "firstName": "Marco",
    "lastName": "Aurelio",
    "username": "marcodev",
    "password": "123$Asdsa",
    "email": "emailteste@gmail.com",
    "subscribedCommunities": [
        {
            "id": { "value": "000000000-0000-0000-000000" },
            "name": "Community name",
            "description": "Community description",
            "topic": "Community topic"
        }
    ],
    "createdAt": "2020-01-01T00:00:00.00000000Z",
    "updatedAt": "2020-01-01T00:00:00.00000000Z"
}
```