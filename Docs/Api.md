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
        - [Get post by id](#get-post-by-id)
            - [Get posts request](#get-post-by-id-request)
            - [Get posts response](#get-post-by-id-response)
        - [Get posts list by community id](#get-posts-list-by-communityid)
            - [Get posts list by community id request](#get-posts-list-by-communityid-request)
            - [Get posts list by community id response](#get-posts-list-by-communityid-response)
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
### Register
```js
POST {{host}}/auth/register
```

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
    "username": "marcoaureliodev",
    "password": "$2a$11$L1QiGeptxaQavEgp0TiinerO0oJUkxLu4Qa63JRJlPyvk4OCx9NES",
    "createdAt": "2024-03-11T15:55:05.8045765Z",
    "updatedAt": "2024-03-11T15:55:05.8045768Z",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...v3M2_2rFe0I-7VTR1NYwGyIR6MEbQydZOAZkyDgHHIQ"
}
```

### Login
```js
POST {{host}}/auth/login
```

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
```js
POST {{host}}/communities/{{userId}}/new-community
```

#### New community request
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
```js
POST {{host}}/communities/{{communityid}}/posts/{{userid}}/new-post
```
#### New post request

```json
{
    "title": "Post title example",
    "content": "Post title content"
}
```

#### New post response
```json
{
    "id": "e663e89d-2708-4858-a14b-8f8b4359d792",
    "communityId": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
    "userId": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
    "title": "Clean code with .net core",
    "content": "This is a example of content",
    "createdAt": "2024-03-13T16:47:41.5176044Z",
    "updatedAt": "2024-03-13T16:47:41.5176045Z",
    "votes": []
}
```

### Get post by id
```js
GET {{host}}/posts/get-post/{{postId}}
```
#### Get post by id request

```json
{
}
```

#### Get post by id response
```json
{
    "post":
    {
        "id": {
            "value": "d7f182d7-f1e7-469d-8c1f-1d0e19a54138"
        },
        "communityId": {
            "value": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"
        },
        "userId": {
            "value": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"
        },
        "title": "Introduction to Cooking",
        "content": "Welcome to our cooking community! Feel free to share your favorite recipes and cooking tips here.",
        "votes": [
            {
                "id": {
                    "value": "9ad89002-3b4b-4797-8a7a-9210c6cf51b5"
                },
                "userId": {
                    "value": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"
                },
                "postId": {
                    "value": "d7f182d7-f1e7-469d-8c1f-1d0e19a54138"
                },
                "isVoted": true
            },
        ]
    }
}
```

### Get posts list by communityId
```js
GET {{host}}/posts/list-posts/{{communityId}}?page=1&pageSize=20
```

#### Get posts list by communityId request
```json
{

}
```

#### Get posts list by communityId response
```json
{
    "posts": [
        {
            "id": {
                "value": "d7f182d7-f1e7-469d-8c1f-1d0e19a54138"
            },
            "communityId": {
                "value": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"
            },
            "userId": {
                "value": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"
            },
            "title": "Introduction to Cooking",
            "content": "Welcome to our cooking community! Feel free to share your favorite recipes and cooking tips here.",
            "votes": [
                {
                    "id": {
                        "value": "9ad89002-3b4b-4797-8a7a-9210c6cf51b5"
                    },
                    "userId": {
                        "value": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"
                    },
                    "postId": {
                        "value": "d7f182d7-f1e7-469d-8c1f-1d0e19a54138"
                    },
                    "isVoted": true
                },
            ]
        }
    ]
}
```

### Update post
```js
PUT {{host}}/posts/update-post/{{postId}}
```
#### Update post request
```json
{
    "userId": "6fc3d752-7ec5-4ca4-a3a6-6d6f41255ac0",
    "title": "Language exchange patterns",
    "content": "Looking for someone to practice Spanish with. Any takers?"
}
```

#### Update post response
```json
{
    "message": "Post updated successfully"
}
```

### Delete post
```js
DELETE {{host}}/posts/delete-post/{{postId}}
```

#### Delete post request
```json
{
    "userId": "6fc3d752-7ec5-4ca4-a3a6-6d6f41255ac0",
}
```

#### Delete post response
```json
{
    "message": "Post deleted successfully"
}
```

### Vote on post
```js
PUT {{host}}/posts/vote-on-post/{{postId}}
```
#### Vote on post request
```json
{
    "UserId": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
    "IsVoted": true
}
```

#### Vote on post response
```json
{
    "message": "Vote on post successfully"
}
```

### Update vote on post
```js
PUT {{host}}/posts/vote-on-post/{{postId}}/update/{{voteId}}
```
#### Update vote on post request
```json
{
    "UserId": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
    "IsVoted": false
}
```

#### Update vote on post response
```json
{
    "Message": "Vote on post updated successfully"
}
```

### Delete vote on post
```js
DELETE {{localhost}}/posts/vote-on-post/{{postId}}/delete/{{voteId}}
```

### Delete vote on post request
```json
{
    "UserId": "a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11",
}
```

### Delete vote on post response
```json
{
    "Message": "Vote on post deleted successfully"
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