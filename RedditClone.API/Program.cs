using RedditClone.API;
using RedditClone.Application;
using RedditClone.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation()
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}