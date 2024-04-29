using Serilog;
using Asp.Versioning;
using System.Reflection;
using Asp.Versioning.Builder;
using RedditClone.Application;
using RedditClone.API.Extension;
using RedditClone.Infrastructure;
using Asp.Versioning.ApiExplorer;
using RedditClone.API.Endpoints.User.Login;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));


    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddApplication(builder.Configuration)
                    .AddInfrastructure(builder.Configuration);

    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
                    .AddApiExplorer(options =>
                    {
                        options.GroupNameFormat = "'v'V";
                        options.SubstituteApiVersionInUrl = true;
                    });

    builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

    builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
}

var app = builder.Build();
{
    ApiVersionSet apiVersionSet = app.NewApiVersionSet()
        .HasApiVersion(new ApiVersion(1))
        .ReportApiVersions()
        .Build();

    RouteGroupBuilder versionedGroup = app.MapGroup("api/v{version:apiVersion}")
        .WithApiVersionSet(apiVersionSet);

    app.MapEndpoints(versionedGroup);

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

            foreach (ApiVersionDescription description in descriptions)
            {
                string url = $"/swagger/{description.GroupName}/swagger.json";
                string name = description.GroupName.ToUpperInvariant();

                options.SwaggerEndpoint(url, name);
            }
        });
    }

    app.UseSerilogRequestLogging();
    app.UseAuthentication();
    app.UseAuthorization();
    app.Run();
}

public partial class Program() { };