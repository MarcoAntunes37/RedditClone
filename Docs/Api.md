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
    - [Reply](#reply)
        - [New reply](#new-reply)
            - [New reply request](#new-reply-request)
            - [New reply response](#new-reply-response)
        - [Get replies](#get-replies)
            - [Get replies request](#get-replies-request)
            - [Get replies response](#get-replies-response)
        - [Update reply](#update-reply)
            - [Update reply request](#update-reply-request)
            - [Update reply response](#update-reply-response)
        - [Delete reply](#delete-reply)
            - [Delete reply request](#delete-reply-request)
            - [Delete reply response](#delete-reply-response)
        - [Upvote reply](#upvote-reply)
            - [Upvote reply request](#upvote-reply-request)
            - [Upvote reply response](#upvote-reply-response)
        - [Downvote reply](#downvote-reply)
            - [Downvote reply request](#downvote-reply-request)
            - [Downvote reply response](#downvote-reply-response)

## Auth

```js
POST {{host}}/auth/register
```

### Register

#### Register Request
```json
{
    "firstName": "Marco",
    "lastName": "Aurelio",
    "username": "marcodev",
    "password": "123$Asdsa",
    "email": "emailteste@gmail.com"
}
```

#### Register Response 
```json
{
    "firstName": "Marco",
    "lastName": "Aurelio"
}
```

### Login

#### Login request
```json
{
    "email": "emailteste@gmail.com ",
    "password": "123$Asdsa"
}
```

#### Login response

```json
{
    "token": "eyJhbG..._adQssw5c"
}
```

## Community

### New community

#### New community request
```js
POST {{host}}/community/new
```
```json
{
    "name": "C#",
    "description": "hello i am a description",
    "members": 1,
    "topic": "Programming",
    "moderators": []
}
```

#### New community response
```json
{
    "message": "New community created."    
}
```

### Get communities

#### Get communities request
```js
GET {{host}}/community/
```
```json
{    
}
```
#### Get communities response
```json
{
    {
        "name": "C#",
        "description": "hello i am a description",
        "members": 1,
        "topic": "Programming",
        "moderators": [
                {
                    "id": "000000000-0000-0000-000000",
                    "username": "marcodev" 
                }
        ]
    },
    ...
}
```

### Update community

#### Update community request
```js
PUT {{host}}/community/{communityId}
```
```json
{
    "name": "C#",
    "description": "hello i am a description",
    "topic": "Programming",
}
```

#### Update community response
```json
{
    "message": "Community updated"
}
```

### Delete community

#### Delete community request
```js
DELETE {{host}}/community/{communityId}
```

#### Delete community response
```json
{
    "message": "Community deleted"
}
```

### Add moderator

#### Add moderator request
```js
POST {{host}}/community/moderator
```

```json
{
    "id": "000000000-0000-0000-000000",
    "username": "marcodev" 
}
```

#### Add moderator response
```json
{
    "message": "Moderator created"
}
```

### Remove moderator

#### Remove moderator request
```js
DELETE {{host}}/community/moderator/{moderator_id}
```

#### Remove moderator response
```json
{
    "message": "Moderator deleted"
}
```

## Post

### New post

#### New post request
```js
POST {{host}}/community/post/new
```
```json
{
    "user_id": "000000000-0000-0000-000000",
    "community_id": "000000000-0000-0000-000000",
    "title": "Post title",
    "content": "Post body",
    "timestamp": "2023-11-13T12:30:00Z"
}
```

#### New post response
```json
{
    "message": "Post created"
}
```
### Get posts

#### Get posts request
```js
GET {{host}}/community/post/
```
```json
{
}
```

#### Get posts response
```json
{
    {
        "title": "Post title",
        "content": "Post body",
        "timestamp": "2023-11-13T12:30:00Z",
        "upvote": 0,
        "downvote": 0
    },
    ...
}
```

### Update post

#### Update post request
```js
PUT {{host}}/community/post/{post_id}
```
```json
{
    "title": "Post title",
    "content": "Post body"
}
```

#### Update post response
```json
{

}
```

### Delete post

#### Delete post request
```js
DELETE {{host}}/community/post/{post_id}
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
PUT {{host}}/community/post/{post_id}/upvote
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
PUT {{host}}/community/post/{post_id}/downvote
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
POST {{host}}/community/post/{post_id}/newcomment
```

```json
{
    "username": "marcodev",
    "content": "Hello i am a content",
    "timestamp": "2023-11-13T12:30:00Z"
}
```

#### New comment response

```json
{
}
```

### Get comments

#### Get comments request
```js
GET {{host}}/community/post/{post_id}/comments
```
```json
{
    
}
```

#### Get comments response
```json
{
    {
        "username": "marcodev",
        "content": "Hello i am a content",
        "timestamp": "2023-11-13T12:30:00Z"
    },
    ...
}
```

### Update comment

#### Update comment request
```js
PUT {{host}}/community/post/{post_id}/comments/{comment_id}
```
```json
{
    "content": "Comment body"
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
DELETE {{host}}/community/post/{post_id}/comments/{comment_id}
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
POST {{host}}/community/post/{post_id}/comments/{comment_id}/upvote
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
POST {{host}}/community/post/{post_id}/comments/{comment_id}/downvote
```
```json
{

}
```

## Reply

### New reply

#### New reply request

#### New reply response

### Get replies

#### Get replies request

#### Get replies response

### Update reply

#### Update reply request

#### Update reply response

### Delete reply

#### Delete reply request

#### Delete reply response

### Upvote reply

#### Upvote reply request

#### Upvote reply response

### Downvote reply

#### Downvote reply request

#### Downvote reply response