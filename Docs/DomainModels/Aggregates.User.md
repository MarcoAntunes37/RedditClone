## User Aggregates

## C# interface
```csharp
    User? GetUserByEmail(string email);
    User? GetUserById(UserId userId);
    User? GetUserByUsername(string username);
    void Add(User user);
    ErrorOr<bool> DeleteUserById(UserId id, UserId requesterId);
    ErrorOr<User> UpdateProfileById(UserId id, string firstname, string lastname, string email);
    ErrorOr<bool> UpdatePasswordById(UserId id, string oldPassword, string newPassword, string matchPassword);
    ErrorOr<bool> UpdateRecoveredPassword(string email, string newPassword, string matchPassword);
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