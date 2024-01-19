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
    "subscribedCommunities": ["000000000-0000-0000-000000"],
    "createdAt": "2020-01-01T00:00:00.00000000Z"
}
```

## Community
```json
{
    "id": "000000000-0000-0000-000000",
    "owner_id": "000000000-0000-0000-000000",
    "name": "C#",
    "description": "hello i am a description",
    "membersCount": 0,
    "topic": "Programming",
    "createdAt": "2020-01-01T00:00:00.00000000Z"
}
```

## Post
```json 
{
    "id": "000000000-0000-0000-000000",
    "userId": "000000000-0000-0000-000000",
    "communityId": "000000000-0000-0000-000000",
    "title": "Post title",
    "content": "Post body",
    "upvote": ["000000000-0000-0000-000000"],
    "downvote": ["000000000-0000-0000-000000"],
    "createdAt": "2020-01-01T00:00:00.00000000Z"
}
```

## Comment
```json
{
    "id": "000000000-0000-0000-000000",
    "postId": "000000000-0000-0000-000000",
    "username": "marcodev",
    "content": "Hello i am a content",
    "upvote": ["000000000-0000-0000-000000"],
    "downvote": ["000000000-0000-0000-000000"],
    "createdAt": "2020-01-01T00:00:00.00000000Z"
}
```

## Reply
```json
{
    "id": "000000000-0000-0000-000000",
    "commentId": "000000000-0000-0000-000000",
    "username": "marcodev",
    "content": "Hello i am a content",
    "upvote": ["000000000-0000-0000-000000"],
    "downvote": ["000000000-0000-0000-000000"],
    "createdAt": "2020-01-01T00:00:00.00000000Z"
}
```