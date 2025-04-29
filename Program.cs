using GoodBurguerAPI.Configurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GoodBurguerAPI");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite($"Filename={dbPath}");
});
builder.Services.AddSwaggerGen();

// TODO: Create the Database builder.

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();