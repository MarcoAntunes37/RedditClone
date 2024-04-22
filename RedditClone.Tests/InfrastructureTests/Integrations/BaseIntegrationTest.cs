using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Bus;
using RedditClone.Infrastructure.Persistence;

namespace RedditClone.Tests.InfrastructureTests.Integrations;

public abstract class BaseIntegrationTests : IClassFixture<IntegrationTestsWebApplicationFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender  _sender;
    protected readonly RedditCloneDbContext _dbContext;
    protected readonly IBus _bus;


    protected BaseIntegrationTests(IntegrationTestsWebApplicationFactory factory)
    {
        _scope = factory.Services.CreateScope();

        _sender = _scope.ServiceProvider.GetRequiredService<ISender>();

        _bus = _scope.ServiceProvider.GetRequiredService<IBus>();

        _dbContext = _scope.ServiceProvider.GetRequiredService<RedditCloneDbContext>();
    }
}