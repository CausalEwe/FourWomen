using AuthorizationService.Application;
using AuthorizationService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration.AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policyBuilder =>
        {
            policyBuilder
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .WithMethods(["GET","POST","PUT","DELETE","OPTIONS"])
                .AllowCredentials();
        });
});

//dbcontext

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

// initializeDb

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseCors("AllowAllOrigins");
await app.RunAsync();