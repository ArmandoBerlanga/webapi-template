using System.Data;

using Microsoft.Data.SqlClient;

using webapi.config;
using webapi.interfaces;


var builder = WebApplication.CreateBuilder(args);

string appsettings = builder.Environment.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json";
builder.Configuration.AddJsonFile(appsettings);
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));
builder.Services.AddTransient<IDapperRepository, DapperRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigJWTParameters();
builder.Services.ConfigSwaggerUI();
builder.Services.ConfigRoutes();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseHttpsRedirection();
app.InitSwaggerUI();
app.AddRoutes();

app.Run();
