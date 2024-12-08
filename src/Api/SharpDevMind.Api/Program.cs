using Serilog;
using SharpDevMind.Api.Extensions;
using SharpDevMind.Api.Middleware;
using SharpDevMind.Common.Application;
using SharpDevMind.Common.Infrastructure;
using SharpDevMind.Modules.Users.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Configuration.AddModuleConfiguration(["users"]);

builder.Services.AddApplication([SharpDevMind.Modules.Users.Application.AssemblyReference.Assembly]);

builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!);

builder.Services.AddUsersModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}
UsersModule.MapEndpoints(app);


app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.Run();

