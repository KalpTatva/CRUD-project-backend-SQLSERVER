

using crud.repository.Models;
using Microsoft.EntityFrameworkCore;
using webapi.Repository.Implementations;
using webapi.Repository.Interfaces;
using webapi.Service.Implementations;
using webapi.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS policy to allow requests from the client application
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigin",
    builder =>
    {
        builder.AllowAnyOrigin()  // Allow all Origins
        .AllowAnyHeader()  // Allow all headers (like Content-Type)
        .AllowAnyMethod(); // Allow all HTTP methods (GET, POST, etc.)
    });
});

builder.Services.AddDbContext<StudentCourseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();


var app = builder.Build();

app.UseCors("AllowAllOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();



// PCA117\SQLEXPRESS