# RedditClone Api

- [RedditClone.API](#RedditClone-api)
    - [Auth](#auth)
        - [Register](#register)
            - [Register request](#register-request)
            - [Register response](#register-response)
        - [Login](#login)
            - [Login request](#login-request)
            - [Login response](#login-response)
    - [Community](#community)
        - [New community](#new-community)
            - [New community request](#new-community-request)
            - [New community response](#new-community-response)
        - [Get communities](#get-communities)
            - [Get communities response](#get-communities-response)
        - [Update community](#update-community)
            - [Update community request](#update-community-request)
            - [Update community response](#update-community-response)
        - [Delete community](#delete-community)
            - [Delete community request](#delete-community-request)
            - [Delete community response](#delete-community-response)
        - [Add moderator](#add-moderator)
            - [Add moderator request](#add-moderator-request)
            - [Add moderator response](#add-moderator-response)
        - [Remove moderator](#remove-moderator)
            - [Remove moderator request](#remove-moderator-request)
            - [Remove moderator response](#remove-moderator-response)
    - [Post](#post)
        - [New post](#new-post)
            - [New post request](#new-post-request)
            - [New post response](#new-post-response)
        - [Get posts](#get-posts)
            - [Get posts request](#get-posts-request)
            - [Get posts response](#get-posts-response)
        - [Update post](#update-post)
            - [Update post request](#update-post-request)
            - [Update post response](#update-post-response)
        - [Delete post](#delete-post)
            - [Delete post request](#delete-post-request)
            - [Delete post response](#delete-post-response)
        - [Upvote post](#upvote-post)
            - [Upvote post request](#upvote-post-request)
            - [Upvote post response](#upvote-post-response)
        - [Downvote post](#downvote-post)
            - [Downvote post request](#downvote-post-request)
            - [Downvote post response](#downvote-post-response)
    - [Comment](#comment)
        - [New comment](#new-comment)
            - [New comment request](#new-comment-request)
            - [New comment response](#new-comment-response)
        - [Get comments](#get-comments)
            - [Get comments request](#get-comments-request)
            - [Get comments response](#get-comments-response)
        - [Update comment](#update-comment)
            - [Update comment request](#update-comment-request)
            - [Update comment response](#update-comment-response)
        - [Delete comment](#delete-comment)
            - [Delete comment request](#delete-comment-request)
            - [Delete comment response](#delete-comment-response)
        - [Upvote comment](#upvote-comment)
            - [Upvote comment request](#upvote-comment-request)
            - [Upvote comment response](#upvote-comment-response)
        - [Downvote comment](#downvote-comment)
            - [Downvote comment request](#downvote-comment-request)
            - [Downvote comment response](#downvote-comment-response)

## Auth

```js
POST {{host}}/auth/register
```

### Register

#### Register Request
```json
{
    "firstname": "User first name",
    "lastname": "User last name",
    "username": "Username",
    "password": "123$Asdsa",
    "email": "emailteste@gmail.com"
}
```

#### Register Response
```json
{
    "firstname": "Marco",
    "lastname": "Aurelio",
    "email": "emailteste@gmail.com",
    "username": "marcodev",
    "password": "A123&!",
    "createdAt": "0001-01-01T00:00:00",
    "updatedAt": "0001-01-01T00:00:00",
    "communities": [],
    "token": "eyJ...aBa8"
}
```

### Login

#### Login request
```json
{
    "email": "emailteste@gmail.com",
    "password": "123$Asdsa"
}
```

#### Login response

```json
{
    "token": "eyJ...aBa8"
}
```

## Community

### New community

#### New community request
```js
POST {{host}}/communities/{{userid}}/new-community
```
```json
{
    "name": "C#",
    "description": "Microsoft C# enjoyers community",
    "membersCount": 1,
    "topic": "Programming"
}
```

#### New community response
```json
{
    "id": "b5c7fad0-f74e-40ed-910e-b3432e9d9af7",
    "userId": "9f7e0a0d-36e0-4239-b600-97aa785c4621",
    "name": "C#",
    "description": "Microsoft C# enjoyers community",
    "membersCount": 1,
    "topic": "Programming",
    "createdAt": "2024-01-30T10:29:32.5521843-03:00",
    "updatedAt": "2024-01-30T10:29:32.5537954-03:00"
}
```

### Get communities

#### Get communities request
```js

```
```json
```
#### Get communities response
```json
```

### Update community

#### Update community request
```js

```
```json
```

#### Update community response
```json
{
}
```

### Delete community

#### Delete community request
```js
```

#### Delete community response
```json
{
}
```

## Post

### New post

#### New post request
```js
POST {{host}}/communities/{{communityid}}/posts/{{userid}}/new-post
```
```json
{
    "title": "Post title example",
    "content": "Post title content"
}
```

#### New post response
```json
{
    "postId": "42fa1dcb-1ed6-45af-be9f-d55a06b8a49f",
    "title": "Post title example",
    "content": "Post title content",
    "userId": "9f7e0a0d-36e0-4239-b600-97aa785c4621",
    "communityId": "e5787ea2-4fd3-4876-88dd-1085760d0d3e",
    "createdAt": "2024-01-30T10:34:25.3111036-03:00",
    "updatedAt": "2024-01-30T10:34:25.3111049-03:00",
    "upvotes": [],
    "downvotes": []
}
```
### Get posts

#### Get posts request
```js
```
```json
{
}
```

#### Get posts response
```json
{
}
```

### Update post

#### Update post request
```js

```
```json

```

#### Update post response
```json
{

}
```

### Delete post

#### Delete post request
```js

```
```json
{
}
```

#### Delete post response
```json
{
}
```

### Upvote post

#### Upvote post request
```js
```
```json
{

}
```

#### Upvote post response
```json
{

}
```

### Downvote post

#### Downvote post request
```js
```
```json
{
}
```

#### Downvote post response
```json
{
}
```

## Comment

### New comment

#### New comment request
```js
POST {{host}}/communities/posts/{{postid}}/{{userid}}/new-comment
```

```json
{
    "content": "Comment content"
}
```

#### New comment response
```json
{
    "id": "567a7342-f5dc-4faa-9101-383e7480c822",
    "userId": "9f7e0a0d-36e0-4239-b600-97aa785c4621",
    "postId": "98590522-931f-486e-9add-f52d0bd2f230",
    "content": "Comment content",
    "createdAt": "2024-01-30T10:37:44.4000587-03:00",
    "updatedAt": "2024-01-30T10:37:44.4000597-03:00",
    "replies": [],
    "upvotes": [],
    "downvotes": []
}
```

### Get comments

#### Get comments request
```js
```
```json
{

}
```

#### Get comments response
```json
{
}
```

### Update comment

#### Update comment request
```js

```
```json
{
}
```

#### Update comment response
```json
{
}
```

### Delete comment

#### Delete comment request
```js
```
```json
{
}
```

#### Delete comment response
```json
{
}
```

### Upvote comment

#### Upvote comment request
```js
```
```json
{

}
```

#### Upvote comment response
```json
{

}
```

### Downvote comment

#### Downvote comment request
```js
```
```json
{

}
```