namespace RedditClone.Application.Behaviors;

using MediatR;
using System.Transactions;
using RedditClone.Application.Common.Interfaces.Persistence;

public sealed class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private static readonly AsyncLocal<TransactionScope?> _currentScope = new();

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (IsNotCommand())
        {
            return await next();
        }

        var currentScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        _currentScope.Value = currentScope;

        try
        {
            var response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            currentScope.Complete();

            return response;
        }
        finally
        {
            _currentScope.Value = null;

            currentScope.Dispose();
        }
    }

    private static bool IsNotCommand()
    {
        return !typeof(TRequest).Name.EndsWith("Command");
    }
}
