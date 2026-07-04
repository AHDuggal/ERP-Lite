using ERPLite.API.Extensions;
using ERPLite.Infrastructure.Extensions;
using ERPLite.Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogLogging();

builder.Services.AddApplicationConfiguration(
    builder.Configuration);


builder.Services.AddDataProtection();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(
    builder.Configuration);
builder.Services.AddApiVersioningConfiguration();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer(
        (document, context, cancellationToken) =>
        {
            document.Info.Title = "ERP Lite API";

            document.Info.Version = "v1";

            document.Info.Description =
                "Enterprise ERP Starter Kit API";

            return Task.CompletedTask;
        });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomMiddleware();
app.MapControllers();

app.Run();
