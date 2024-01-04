# Domain Models

## User
```json
{
    "id": "000000000-0000-0000-000000",
    "firstName": "Marco",
    "lastName": "Aurelio",
    "username": "marcodev",
    "password": "123$Asdsa",
    "email": "emailteste@gmail.com",
    "subscribed": ["000000000-0000-0000-000000"]
}
```

## Community
```json
{
    "id": "000000000-0000-0000-000000",
    "owner_id": "000000000-0000-0000-000000",
    "name": "C#",
    "description": "hello i am a description",
    "members": 1,
    "topic": "Programming"    
}
```

## Moderator
```json
{
    "id": "000000000-0000-0000-000000",
    "user_id": "000000000-0000-0000-000000",
    "community_id": "000000000-0000-0000-000000",
    "username": "marcodev"
}
```

## Post
```json 
{
    "id": "000000000-0000-0000-000000",
    "user_id": "000000000-0000-0000-000000",
    "community_id": "000000000-0000-0000-000000",
    "title": "Post title",
    "content": "Post body",
    "timestamp": "2023-11-13T12:30:00Z",
    "upvote": 0,
    "downvote": 0
}
```

## Comment
```json
{
    "id": "000000000-0000-0000-000000",
    "post_id": "000000000-0000-0000-000000",
    "username": "marcodev",
    "content": "Hello i am a content",
    "timestamp": "2023-11-13T12:30:00Z"
}
```

## Reply
```json
{
    "id": "000000000-0000-0000-000000",
    "comment_id": "000000000-0000-0000-000000",
    "username": "marcodev",
    "content": "Hello i am a content",
    "timestamp": "2023-11-13T12:30:00Z"
}
```