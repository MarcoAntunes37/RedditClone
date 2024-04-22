namespace RedditClone.Application.User.Commands.UpdateProfile;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.UpdateProfile;

public partial class UpdateProfileCommandHandler(
    IUserRepository userRepository)
        : IRequestHandler<UpdateProfileCommand, ErrorOr<UpdateProfileResult>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorOr<UpdateProfileResult>> Handle(
        UpdateProfileCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@UpdateProfileCommand}",
            "Trying update profile of {@UserId}",
            command,
            command.UserId);

        var user = _userRepository.UpdateProfileById(
            command.UserId,
            command.Firstname,
            command.Lastname,
            command.Email);

        UpdateProfileResult result = new(user.Value);

        Log.Information(
            "{@UpdateProfileResult}",
            result);

        return result;
    }
}