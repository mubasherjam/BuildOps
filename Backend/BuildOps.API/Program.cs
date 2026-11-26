using BuildOps.Application.Interfaces;
using BuildOps.Application.Services;
using BuildOps.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();

// Register OpenAPI
builder.Services.AddOpenApi();

// Register Infrastructure services
builder.Services.AddInfrastructure(
    builder.Configuration);

// Register Application services
builder.Services.AddScoped<
    IEquipmentCategoryService,
    EquipmentCategoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
