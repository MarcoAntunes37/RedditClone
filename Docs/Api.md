# ListTodo Api

- [ListTodo.API](#listtodo-api)
    - [Auth](#auth)
        - [Register](#register)
            - [Register request](#register-request)
            - [Register response](#register-response)
        - [Login](#login)
            - [Login request](#login-request)
            - [login response](#login-response)

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