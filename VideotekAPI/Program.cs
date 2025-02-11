using Microsoft.EntityFrameworkCore;
using ModelVideotek.Contexts;

var builder = WebApplication.CreateBuilder(args);

var mainConnectionString = builder.Configuration.GetConnectionString("Database") ?? 
    throw new Exception("Connection string is missing");
builder.Services.AddDbContext<VideosDbContext>(opt =>
    opt.UseSqlServer(mainConnectionString));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
