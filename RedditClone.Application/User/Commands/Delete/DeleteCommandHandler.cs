namespace RedditClone.Application.User.Commands.Delete;

using MediatR;
using RedditClone.Application.Persistence;
using FluentValidation;
using RedditClone.Application.User.Results;

public partial class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, DeleteResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<DeleteUserCommand> _validator;

    public DeleteUserCommandHandler(
        IUserRepository userRepository,
        IValidator<DeleteUserCommand> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<DeleteResult> Handle(DeleteUserCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _userRepository.DeleteUserById(command.UserId);

        return new DeleteResult("User deleted successfully");
    }
}