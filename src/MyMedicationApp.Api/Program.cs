using Microsoft.EntityFrameworkCore;
using MyMedicationApp.Infrastructure.Data;
using MyMedicationApp.Infrastructure.Repositories;
using MyMedicationApp.Application.Interfaces;
using MyMedicationApp.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// 1) Configure Database Connection (using PostgreSQL with Npgsql)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Host=localhost;Database=MyMedicationDb;Username=postgres;Password=postgres;";

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2) Register Repositories and Services
builder.Services.AddScoped<IMedicationRepository, MedicationRepository>();
builder.Services.AddScoped<MedicationService>();

// 3) Add Controllers, Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();