## User Aggregates

## C# interface
```csharp
    void Register(User user);
    void Login(string username, string password);
    void Update(User user);
    void SubscribeCommunity(Guid communityid);
    void Delete(Guid userId);
```


## Domain object representation
```json
{
    "id": { "value": "000000000-0000-0000-000000" },
    "firstname": "Marco",
    "lastname": "Aurelio",
    "username": "marcodev",
    "password": "123$Asdsa",
    "email": "emailteste@gmail.com",
    "createdAt": "2020-01-01T00:00:00.00000000Z",
    "updatedAt": "2020-01-01T00:00:00.00000000Z"
}
```

## Post register user request

### Route
```js
POST {{host}}/auth/register
```

### Payload
```json
{
    "firstname": "Marco",
    "lastname": "Aurelio",
    "username": "marcodev",
    "password": "123$Asdsa",
    "email": "emailteste@gmail.com",
    "communities": [],
}
```

## Post login user request

### Route
```js
POST {{host}}/auth/login
```

### Payload
```json
{
    "password": "123$Asdsa",
    "email": "emailteste@gmail.com"
}
```