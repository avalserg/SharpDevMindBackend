using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using SharpDevMind.Api.Extensions;
using SharpDevMind.Api.Middleware;
using SharpDevMind.Api.OpenTelemetry;
using SharpDevMind.Common.Application;
using SharpDevMind.Common.Infrastructure;
using SharpDevMind.Common.Presentation.Endpoints;
using SharpDevMind.Modules.Comments.Infrastructure;
using SharpDevMind.Modules.Posts.Infrastructure;
using SharpDevMind.Modules.Users.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Configuration.AddModuleConfiguration(["users", "posts", "comments"]);

builder.Services.AddApplication([
    SharpDevMind.Modules.Users.Application.AssemblyReference.Assembly,
    SharpDevMind.Modules.Posts.Application.AssemblyReference.Assembly,
    SharpDevMind.Modules.Comments.Application.AssemblyReference.Assembly
]);

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;

builder.Services.AddInfrastructure(
    DiagnosticsConfig.ServiceName,
    [PostsModule.ConfigureConsumers,
        CommentsModule.ConfigureConsumers],
    databaseConnectionString,
    redisConnectionString);


builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString)
    .AddUrlGroup(
        new Uri(builder.Configuration.GetValue<string>("KeyCloak:HealthUrl")!),
        HttpMethod.Get, "keycloak");

builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddPostsModule(builder.Configuration);
builder.Services.AddCommentsModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseLogContextTraceLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints();

app.Run();

