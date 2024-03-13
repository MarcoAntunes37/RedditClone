# Domain Models

## User
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

## Community
```json
{
    "id": { "value": "000000000-0000-0000-000000" },
    "userId": { "value": "000000000-0000-0000-000000" },
    "name": "C#",
    "description": "Microsoft C# enjoyers community",
    "topic": "Programming",
    "createdAt": "2020-01-01T00:00:00.00000000Z",
    "updatedAt": "2020-01-01T00:00:00.00000000Z"
}
```

## UserCommunities
```json
{
    "userId": { "value": "000000000-0000-0000-000000" },
    "communityId": { "value": "000000000-0000-0000-000000" }
}
```

## Post
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
            "isVoted": true
        }],
    "createdAt": "2020-01-01T00:00:00.00000000Z",
    "updatedAt": "2020-01-01T00:00:00.00000000Z"
}
```

## Comment
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
            "isVoted": true
        }],
    "replies": [
        {
            "id": { "value": "000000000-0000-0000-000000"},
            "userId":  { "value": "000000000-0000-0000-000000" },
            "content": "some content",
            "votes": [
                {
                    "id": { "value": "000000000-0000-0000-000000" },
                    "userId": { "value": "000000000-0000-0000-000000" },
                    "isVoted": true
                }],
        }
    ],
    "createdAt": "2020-01-01T00:00:00.00000000Z",
    "updatedAt": "2020-01-01T00:00:00.00000000Z"
}
```
