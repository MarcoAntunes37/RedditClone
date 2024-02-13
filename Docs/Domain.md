# Domain Models

## User
```json
{
    "id": "000000000-0000-0000-000000",
    "firstname": "Marco",
    "lastname": "Aurelio",
    "username": "marcodev",
    "password": "123$Asdsa",
    "email": "emailteste@gmail.com",
    "communities": [
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

## Community
```json
{
    "id": { "value": "000000000-0000-0000-000000"},
    "userId": { "value": "000000000-0000-0000-000000"},
    "name": "C#",
    "description": "Microsoft C# enjoyers community",
    "membersCount": 0,
    "topic": "Programming",
    "createdAt": "2020-01-01T00:00:00.00000000Z",
    "updatedAt": "2020-01-01T00:00:00.00000000Z"
}
```

## Post
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

## Comment
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
