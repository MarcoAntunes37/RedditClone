namespace ListToDo.Application.Services.Authentication.Responses;

public record RegisterResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);