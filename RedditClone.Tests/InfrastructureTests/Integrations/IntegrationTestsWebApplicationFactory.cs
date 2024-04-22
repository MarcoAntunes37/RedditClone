namespace RedditClone.Tests.InfrastructureTests.Integrations;

using Rebus.Bus;
using Rebus.Config;
using System.Threading.Tasks;
using Testcontainers.RabbitMq;
using Testcontainers.PostgreSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using RedditClone.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using DotNet.Testcontainers.Builders;

public class IntegrationTestsWebApplicationFactory
    : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("DB")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly RabbitMqContainer _mqContainer = new RabbitMqBuilder()
        .WithImage("rabbitmq:3-management")
        .WithUsername("guest")
        .WithPassword("guest")
        .WithPortBinding(5672, 5672)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var dbContext = services
                .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<RedditCloneDbContext>));

            if (dbContext != null)
            {
                services.Remove(dbContext);
            }

            services.RemoveAll<IBus>();

            services.RemoveAll<Rebus.Transport.ITransport>();

            services.AddDbContext<RedditCloneDbContext>(options =>
            {
                options
                    .UseNpgsql(_dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention();
            });

            services.AddRebus(configure => configure
                .Logging(l => l.Serilog())
                .Transport(t =>
                   t.UseRabbitMqAsOneWayClient("amqp://guest:guest@localhost:5672")), true);
        });
    }

    public Task InitializeAsync()
    {
        _mqContainer.StartAsync();

        _dbContainer.StartAsync();

        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        _mqContainer.StopAsync();

        _dbContainer.StopAsync();

        return Task.CompletedTask;
    }
}