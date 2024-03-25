using RedditClone.API;
using RedditClone.API.Middlewares;
using RedditClone.Application;
using RedditClone.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    builder.Services.AddPresentation()
                    .AddApplication(builder.Configuration)
                    .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseSerilogRequestLogging();
    app.UseMiddleware<ExceptionHandlerMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}