using Microsoft.EntityFrameworkCore;
using Serilog;
using UserApi.Middlewares;
using UserApi.Models;
using UserApi.UserData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Add DB service
var connectionString = builder
    .Configuration
    .GetConnectionString("UserContextConnectionString");

var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));

builder.Services.AddDbContextPool<UserContext>(
    options => options.UseMySql(connectionString, serverVersion));


//builder.Services.AddSingleton<IUserData, MockUserData>();
builder.Services.AddScoped<IUserData, SqlUserData>();

// Serilog setup
//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json")
//    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();



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

//app.UseSerilogRequestLogging();

// Custom Middleware
app.UseRequestTimeLogging();


try
{
    Log.Information("Starting app.");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "App startup failed.");
}
finally
{
    Log.CloseAndFlush();
}