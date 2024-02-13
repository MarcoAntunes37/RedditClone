namespace RedditClone.Application.User.Commands.UpdateProfile;

using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.User.Results.UpdateProfile;

public record UpdateProfileCommand(
    UserId UserId,
    string Firstname,
    string Lastname,
    string Email
) : IRequest<UpdateProfileResult>;