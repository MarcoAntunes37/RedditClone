using RedditClone.API;
using RedditClone.API.Middlewares;
using RedditClone.Application;
using RedditClone.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation()
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseMiddleware<ExceptionHandlerMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}