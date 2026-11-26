using BuildOps.Application.Interfaces;
using BuildOps.Application.Services;
using BuildOps.Infrastructure;
using BuildOps.API.ExceptionHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

// Exception handling registration
builder.Services.AddProblemDetails();

builder.Services.AddExceptionHandler<
    GlobalExceptionHandler>();

// Infrastructure registration
builder.Services.AddInfrastructure(
    builder.Configuration);

// Application registration
builder.Services.AddScoped<
    IEquipmentCategoryService,
    EquipmentCategoryService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Exception handling middleware
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AngularClient");

app.UseAuthorization();

app.MapControllers();

app.Run();