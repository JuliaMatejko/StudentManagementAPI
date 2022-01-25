using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Get settings from appsettings.json StudentStoreDatabaseSettings section
// and map them to StudentStoreDatabaseSettings class
builder.Services.Configure<StudentStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(StudentStoreDatabaseSettings)));

// Bind IStudentStoreDatabaseSettings interface with StudentStoreDatabaseSettings class
// Meaning, whenever IStudentStoreDatabaseSettings interface is required
// provide instance of StudentStoreDatabaseSettings class
builder.Services.AddSingleton<IStudentStoreDatabaseSettings>(sp =>
sp.GetRequiredService<IOptions<StudentStoreDatabaseSettings>>().Value);

//Specify MongoClient
builder.Services.AddSingleton<IMongoClient>(s =>
new MongoClient(builder.Configuration.GetValue<string>("StudentStoreDatabaseSettings:ConnectionString")));

//Bind IStudentService interface with its implementation, StudentService class
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
