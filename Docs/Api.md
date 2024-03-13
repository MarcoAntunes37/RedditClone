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
            - [Get communities parameters](#parameters)
            - [Get communities response](#get-communities-request)
            - [Get communities response](#get-communities-response)
        - [Update community](#update-community)
            - [Update community request](#update-community-request)
            - [Update community response](#update-community-response)
        - [Delete community](#delete-community)
            - [Delete community request](#delete-community-request)
            - [Delete community response](#delete-community-response)
        - [Transfer ownership](#transfer-ownership)
            - [Transfer ownership request](#transfer-ownership-request)
            - [Transfer ownership response](#transfer-ownership-response)
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

## Auth

```js
POST {{host}}/auth/register
```

### Register

#### Register Request
```json
{
    "firstname": "Marco",
    "lastname": "Aurelio",
    "email": "joloma5537@rohoza.com",
    "username": "kgbstrike",
    "password": "A123&!aaaaa",
    "repeatPassword": "A123&!aaaaa"
}
```

#### Register Response
```json
{
    "firstname": "Marco",
    "lastname": "Aurelio",
    "email": "joloma5537@rohoza.com",
    "username": "kgbstrike",
    "password": "$2a$11$L1QiGeptxaQavEgp0TiinerO0oJUkxLu4Qa63JRJlPyvk4OCx9NES",
    "createdAt": "2024-03-11T15:55:05.8045765Z",
    "updatedAt": "2024-03-11T15:55:05.8045768Z",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...v3M2_2rFe0I-7VTR1NYwGyIR6MEbQydZOAZkyDgHHIQ"
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
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...v3M2_2rFe0I-7VTR1NYwGyIR6MEbQydZOAZkyDgHHIQ"
}
```

## Community

### New community

#### New community request
```js
POST {{host}}/communities/{{userId}}/new-community
```
```json
{
    "name": "C#",
    "description": "Microsoft C# enjoyers community",
    "topic": "Programming"
}
```

#### New community response
```json
{
    "message": "Community created successfully",
    "name": "C#",
    "description": "Microsoft C# enjoyers community",
    "topic": "Programming",
    "createdAt": "2024-01-30T10:29:32.5521843-03:00",
    "updatedAt": "2024-01-30T10:29:32.5537954-03:00"
}
```

### Get communities
```js
GET {{host}}/communities/list-communities?name=C#&topic=Programming&page=1&pageSize=20,
```

#### Parameters
- Name(optional): This parameter allows you to filter communities by name. If provided, only communities with names containing the specified string will be returned.

- Topic(optional): This parameter allows you to filter communities by topic. If provided, only communities with the specified topic will be returned.

- Page(optional): This parameter specifies the desired page number in the results list. Each page contains a fixed number of communities.

- PageSize (optional): This parameter specifies the maximum number of communities that will be returned per page. If not specified, the default page size will be used.

#### Get communities request
```json
{
    "name": "C#",
    "topic": "Programming",
    "page": 1,
    "pageSize": 20
}
```
#### Get communities response
```json
{
    "communities": [
        {
            "name": "C#",
            "description": "Microsoft C# enjoyers community",
            "topic": "Programming"
        },
        {
            "name": "C#1",
            "description": "Microsoft C#1 enjoyers community",
            "topic": "Programming"
        }
    ]
}
```

### Update community
```js
POST {{host}}/communities/update-community/{{communityId}}
```
#### Update community request
```json
{
    "userId": "04d64e02-c25a-4238-8519-50e924552984",
    "name": "C#",
    "description": "Microsoft C# enjoyers community",
    "topic": "Programming"
}
```

#### Update community response
```json
{
    "message": "Community update successfully",
    "name": "C#",
    "description": "Microsoft C# enjoyers community",
    "topic": "Programming"
}
```

### Delete community
```js
DELETE {{host}}/communities/delete-community/{{communityId}}
```

#### Delete community request
```json
{
    "userId": "04d64e02-c25a-4238-8519-50e924552984"
}
```

#### Delete community response
```json
{
    "Message": "Community deleted successfully"
}
```

### Transfer ownership
```js
POST {{host}}/communities/update-community/transfer/{{communityId}}
```

#### Transfer ownership request
```json
{
    "userId": "04d64e02-c25a-4238-8519-50e924552984",
    "newUserId": "d7524584-e5b7-43a2-af56-27d97cf59e6a"
}
```

#### Transfer ownership response
```json
{
    "Message": "Community successfully transferred"
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