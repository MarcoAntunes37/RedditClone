namespace RedditClone.Application.User.Commands.Delete;

using MediatR;
using RedditClone.Application.Persistence;
using FluentValidation;
using RedditClone.Application.User.Results;

public partial class DeleteCommandHandler
    : IRequestHandler<DeleteCommand, DeleteResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<DeleteCommand> _validator;

    public DeleteCommandHandler(
        IUserRepository userRepository,
        IValidator<DeleteCommand> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<DeleteResult> Handle(DeleteCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _userRepository.DeleteUserById(command.UserId);

        return new DeleteResult("User deleted successfully");
    }
}