using ERPLite.API.Extensions;
using ERPLite.API.Filters;
using ERPLite.Application.DependencyInjection;
using ERPLite.Infrastructure.Extensions;
using ERPLite.Infrastructure.Identity;



var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogLogging();

builder.Services.AddDataProtection();
builder.Services.AddScoped<ValidationFilter>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApiVersioningConfiguration();

builder.Services.AddAuthorizationConfiguration();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ERP Lite API",
        Version = "v1",
        Description = "Enterprise ERP Starter Kit API"
    });

    options.AddSecurityDefinition(
        "Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter: Bearer {JWT Token}"
        });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference =
                        new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type =
                                Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,

                            Id = "Bearer"
                        }
                },

                Array.Empty<string>()
            }
        });
});


// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});


var app = builder.Build();

using (var scope =
    app.Services.CreateScope())
{
    await IdentitySeeder.SeedAsync(
        scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "ERP Lite API v1");

        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseCustomMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
